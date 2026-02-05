using App.Modules.Sys.Shared.Models.Persistence;

namespace App.Modules.Sys.Domain.Domains.Workspaces.Models;

/// <summary>
/// Represents a user's membership in a workspace.
/// This is an entity (not an aggregate root) - managed through Workspace aggregate.
/// </summary>
public class WorkspaceMember : IHasGuidId
{
    /// <summary>
    /// Unique membership identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Workspace this membership belongs to.
    /// </summary>
    public Guid WorkspaceId { get; set; }

    /// <summary>
    /// User ID (references Sys.User or Social.Person depending on architecture).
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// User's role in this workspace (Owner, Admin, Member, Guest).
    /// </summary>
    public string Role { get; set; } = "Member";

    /// <summary>
    /// Whether this is the user's default/primary workspace.
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// When the user joined this workspace.
    /// </summary>
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Navigation property to workspace.
    /// </summary>
    public Workspace Workspace { get; set; } = null!;
}
