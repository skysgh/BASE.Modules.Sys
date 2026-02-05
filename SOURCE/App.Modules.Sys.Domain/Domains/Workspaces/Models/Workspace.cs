using App.Modules.Sys.Shared.Models;
using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Domain.Domains.Workspaces.Models;

/// <summary>
/// Workspace aggregate root - represents a tenant/organization workspace.
/// A workspace is a multi-tenant isolation boundary with its own settings, branding, and members.
/// </summary>
public class Workspace : IHasGuidId, IHasTitleAndDescription
{
    /// <summary>
    /// Unique workspace identifier (GUID).
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Workspace display name.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Workspace description/purpose.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// URL-friendly slug for the workspace (e.g., "acme-corp").
    /// </summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Organization name for branding.
    /// </summary>
    public string OrganizationName { get; set; } = string.Empty;

    /// <summary>
    /// Logo URL for workspace branding.
    /// </summary>
    public string? LogoUrl { get; set; }

    /// <summary>
    /// Primary brand color (hex code).
    /// </summary>
    public string? PrimaryColor { get; set; }

    /// <summary>
    /// Custom CSS URL for advanced branding.
    /// </summary>
    public string? CustomCssUrl { get; set; }

    /// <summary>
    /// Subscription tier (Free, Pro, Enterprise).
    /// </summary>
    public string SubscriptionTier { get; set; } = "Free";

    /// <summary>
    /// Maximum number of users allowed in this workspace.
    /// Null = unlimited.
    /// </summary>
    public int? MaxUsers { get; set; }

    /// <summary>
    /// Storage limit in GB.
    /// Null = unlimited.
    /// </summary>
    public int? StorageLimitGb { get; set; }

    /// <summary>
    /// Whether this workspace is active (not suspended/deleted).
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// When this workspace was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// When this workspace was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Members of this workspace (users with roles).
    /// </summary>
    public List<WorkspaceMember> Members { get; set; } = new();

    /// <summary>
    /// Workspace-specific settings (JSON key-value pairs).
    /// </summary>
    public string? SettingsJson { get; set; }

    // ========================================
    // BUSINESS LOGIC / DOMAIN METHODS
    // ========================================

    /// <summary>
    /// Checks if the workspace can accept more members based on subscription tier.
    /// </summary>
    public bool CanAddMember()
    {
        if (!IsActive)
        {
            return false;
        }

        if (MaxUsers == null)
        {
            return true; // Unlimited
        }

        return Members.Count < MaxUsers.Value;
    }

    /// <summary>
    /// Adds a member to the workspace.
    /// </summary>
    /// <param name="userId">User ID to add.</param>
    /// <param name="role">Role in the workspace.</param>
    /// <param name="isDefault">Whether this is the user's default workspace.</param>
    /// <exception cref="InvalidOperationException">If workspace cannot accept more members.</exception>
    public void AddMember(Guid userId, string role, bool isDefault = false)
    {
        if (!CanAddMember())
        {
            throw new InvalidOperationException($"Workspace {Id} cannot accept more members (limit: {MaxUsers})");
        }

        var member = new WorkspaceMember
        {
            Id = Guid.NewGuid(),
            WorkspaceId = Id,
            UserId = userId,
            Role = role,
            IsDefault = isDefault,
            JoinedAt = DateTime.UtcNow
        };

        Members.Add(member);
    }

    /// <summary>
    /// Removes a member from the workspace.
    /// </summary>
    public void RemoveMember(Guid userId)
    {
        var member = Members.FirstOrDefault(m => m.UserId == userId);
        if (member != null)
        {
            Members.Remove(member);
        }
    }

    /// <summary>
    /// Updates a member's role.
    /// </summary>
    public void UpdateMemberRole(Guid userId, string newRole)
    {
        var member = Members.FirstOrDefault(m => m.UserId == userId);
        if (member != null)
        {
            member.Role = newRole;
        }
    }
}
