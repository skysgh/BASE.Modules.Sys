using System;
using System.ComponentModel.DataAnnotations;

namespace App.Modules.Sys.Domain.Settings;

/// <summary>
/// Setting entity supporting hierarchical cascade (System → Workspace → User).
/// Stores configuration values at different scopes with override support.
/// </summary>
/// <remarks>
/// Cascade Resolution:
/// - Read: User.Value ?? Workspace.Value ?? System.Value (first non-null wins)
/// - Locked settings prevent lower-level overrides
/// - Each scope level can be independently queried or modified
/// 
/// Examples:
/// - System: theme=light (global baseline)
/// - Workspace: theme=corporate-blue (workspace override)
/// - User: theme=dark (personal preference) ← This is what user sees
/// </remarks>
public class Setting
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Setting key (e.g., "theme", "language", "pageSize").
    /// Case-insensitive, unique per scope+workspace+user.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Key { get; set; } = null!;

    /// <summary>
    /// Setting value (stored as string, can be JSON for complex types).
    /// </summary>
    [Required]
    public string Value { get; set; } = null!;

    /// <summary>
    /// Value type hint for deserialization (e.g., "string", "int", "bool", "json").
    /// </summary>
    [MaxLength(50)]
    public string? ValueType { get; set; }

    /// <summary>
    /// Scope level (System=1, Workspace=2, User=3).
    /// Determines hierarchy level for cascade resolution.
    /// </summary>
    [Required]
    public SettingScope Scope { get; set; }

    /// <summary>
    /// Workspace ID (null for System scope, required for Workspace/User scopes).
    /// </summary>
    public Guid? WorkspaceId { get; set; }

    /// <summary>
    /// User ID (null for System/Workspace scopes, required for User scope).
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Whether this setting is locked at this level.
    /// Locked settings cannot be overridden by lower scopes.
    /// </summary>
    /// <remarks>
    /// Example: System admin locks "dataRetentionDays" → workspace/user cannot override
    /// </remarks>
    public bool IsLocked { get; set; }

    /// <summary>
    /// Optional description/help text for admin UIs.
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Optional category for grouping in UI (e.g., "Appearance", "Security").
    /// </summary>
    [MaxLength(100)]
    public string? Category { get; set; }

    /// <summary>
    /// When this setting was created.
    /// </summary>
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// When this setting was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Who created this setting (for audit trail).
    /// </summary>
    public Guid? CreatedByUserId { get; set; }

    /// <summary>
    /// Who last updated this setting (for audit trail).
    /// </summary>
    public Guid? UpdatedByUserId { get; set; }

    /// <summary>
    /// Validation: Ensure scope-specific IDs are set correctly.
    /// </summary>
    public bool IsValid()
    {
        return Scope switch
        {
            SettingScope.System => WorkspaceId == null && UserId == null,
            SettingScope.Workspace => WorkspaceId != null && UserId == null,
            SettingScope.User => WorkspaceId != null && UserId != null,
            _ => false
        };
    }

    /// <summary>
    /// Get unique composite key for this setting (Scope+Workspace+User+Key).
    /// </summary>
    public string GetCompositeKey()
    {
        return $"{Scope}:{WorkspaceId?.ToString() ?? "null"}:{UserId?.ToString() ?? "null"}:{Key}";
    }
}

/// <summary>
/// Setting scope levels for hierarchical cascade.
/// Lower numbers = higher priority in baseline (but overridden by higher numbers).
/// </summary>
public enum SettingScope
{
    /// <summary>
    /// System-wide baseline (global defaults, admin only).
    /// </summary>
    System = 1,

    /// <summary>
    /// Workspace-level overrides (workspace-specific defaults).
    /// </summary>
    Workspace = 2,

    /// <summary>
    /// User-level overrides (personal preferences).
    /// </summary>
    User = 3
}
