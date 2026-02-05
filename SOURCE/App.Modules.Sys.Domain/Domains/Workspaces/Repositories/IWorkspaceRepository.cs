using App.Modules.Sys.Domain.Domains.Workspaces.Models;

namespace App.Modules.Sys.Domain.Domains.Workspaces.Repositories;

/// <summary>
/// Repository contract for Workspace aggregate.
/// </summary>
public interface IWorkspaceRepository
{
    /// <summary>
    /// Gets a workspace by ID including its members.
    /// </summary>
    /// <param name="workspaceId">Workspace identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Workspace with members, or null if not found.</returns>
    Task<Workspace?> GetByIdAsync(Guid workspaceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all workspaces where the specified user is a member.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of workspaces the user belongs to.</returns>
    Task<List<Workspace>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the user's default workspace.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Default workspace, or null if none set.</returns>
    Task<Workspace?> GetDefaultWorkspaceAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new workspace.
    /// </summary>
    /// <param name="workspace">Workspace to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task AddAsync(Workspace workspace, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing workspace.
    /// </summary>
    /// <param name="workspace">Workspace to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task UpdateAsync(Workspace workspace, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a workspace (soft delete by setting IsActive = false).
    /// </summary>
    /// <param name="workspaceId">Workspace identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task DeleteAsync(Guid workspaceId, CancellationToken cancellationToken = default);
}
