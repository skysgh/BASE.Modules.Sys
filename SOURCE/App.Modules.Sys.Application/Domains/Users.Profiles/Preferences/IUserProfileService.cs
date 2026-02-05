using App.Modules.Sys.Application.Domains.Context.Models.Implementations;
using App.Modules.Sys.Shared.Lifecycles;
using App.Modules.Sys.Shared.Services;

namespace App.Modules.Sys.Application.Domains.Users.Profiles.Preferences;

/// <summary>
/// Service for managing user system profiles (preferences).
/// Auto-registered via IHasScopedLifecycle.
/// </summary>
public interface IUserProfileService : IHasService
{
    /// <summary>
    /// Gets the system profile for the specified user.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>User system profile DTO, or null if not found.</returns>
    Task<SystemProfileDto?> GetSystemProfileAsync(string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the system profile for the specified user.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="profile">Updated profile data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task UpdateSystemProfileAsync(string userId, SystemProfileDto profile, CancellationToken cancellationToken = default);
}


