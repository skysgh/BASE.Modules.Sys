using App.Modules.Sys.Shared.Services;

namespace App.Modules.Sys.Substrate.Contracts.Social;

/// <summary>
/// Service to resolve person identity for the current user or any user.
/// 
/// This contract is defined in Sys so that:
/// 1. Context service can always get a display name for the current user
/// 2. Social module provides full implementation when available
/// 3. Sys.Infrastructure provides lite fallback using User claims
/// 
/// RESOLUTION LOGIC:
/// 
/// Lite Mode (Social not deployed):
/// - User claims (name, preferred_username, email, picture) from IdP
/// - Single identity per user
/// - No relationships, groups, or multiple identities
/// 
/// Full Mode (Social deployed):
/// - PersonToUserAssignment → Person → PersonIdentity
/// - Workspace membership may specify which identity to show
/// - Falls back to primary PersonIdentity
/// </summary>
public interface IPersonIdentityResolverService : IHasScopedService
{
    /// <summary>
    /// Get identity to display for the current authenticated user
    /// in the current workspace context.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Identity info, or null if not authenticated.</returns>
    Task<IPersonIdentityInfo?> GetCurrentUserIdentityAsync(CancellationToken ct = default);

    /// <summary>
    /// Get identity to display for a specific user
    /// in the current workspace context.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Identity info, or null if user not found.</returns>
    Task<IPersonIdentityInfo?> GetIdentityForUserAsync(Guid userId, CancellationToken ct = default);

    /// <summary>
    /// Get identity to display for a specific user in a specific workspace.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="workspaceId">The workspace ID.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Identity info, or null if user not found.</returns>
    Task<IPersonIdentityInfo?> GetIdentityForUserInWorkspaceAsync(
        Guid userId, 
        Guid workspaceId, 
        CancellationToken ct = default);

    /// <summary>
    /// Whether this resolver is running in lite mode (no Social module)
    /// or full mode (Social module deployed).
    /// </summary>
    bool IsLiteMode { get; }
}
