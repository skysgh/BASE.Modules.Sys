using System.ComponentModel.DataAnnotations;

namespace App.Modules.Sys.Domain.ReferenceData;

/// <summary>
/// Associates a SystemLanguage with a Workspace.
/// Enables workspace-specific language availability.
/// 
/// Architecture:
/// - System Level: SystemLanguage.IsActive determines global availability
/// - Workspace Level: WorkspaceLanguageAssignment enables subset per workspace
/// - User Level: User.PreferredLanguageCode stores individual preference
/// </summary>
public class WorkspaceLanguageAssignment
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The workspace this assignment belongs to.
    /// </summary>
    public Guid WorkspaceId { get; set; }

    /// <summary>
    /// The language code (ISO 639-1) being assigned.
    /// References SystemLanguage.Code.
    /// </summary>
    [Required]
    [MaxLength(10)]
    public string LanguageCode { get; set; } = string.Empty;

    /// <summary>
    /// Whether this is the default language for this workspace.
    /// Only one language per workspace should be default.
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// Display order within the workspace's language selector.
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// When this assignment was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// When this assignment was last modified.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    // Note: Navigation to Workspace would create circular dependency
    // Keep this lightweight - use repository joins when needed
}
