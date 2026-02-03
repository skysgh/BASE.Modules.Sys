using App.Modules.Sys.Domain.Authorization;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Application.Services.Authorization
{
    /// <summary>
    /// Application service for managing user permission assignments.
    /// Handles granting/revoking system permissions to/from users.
    /// </summary>
    public interface IUserPermissionService
    {
        /// <summary>
        /// Grant system permission to user.
        /// </summary>
        /// <param name="userId">User to grant permission to</param>
        /// <param name="permissionKey">Permission key (e.g., "System.Configure")</param>
        /// <param name="grantedBy">Who granted the permission (for audit)</param>
        /// <param name="ct">Cancellation token</param>
        Task GrantPermissionAsync(Guid userId, string permissionKey, string grantedBy, CancellationToken ct = default);

        /// <summary>
        /// Revoke system permission from user.
        /// </summary>
        Task RevokePermissionAsync(Guid userId, string permissionKey, CancellationToken ct = default);

        /// <summary>
        /// Get all permissions assigned to a user.
        /// </summary>
        Task<IEnumerable<SystemPermission>> GetUserPermissionsAsync(Guid userId, CancellationToken ct = default);

        /// <summary>
        /// Check if user has specific permission.
        /// </summary>
        Task<bool> HasPermissionAsync(Guid userId, string permissionKey, CancellationToken ct = default);

        /// <summary>
        /// Check if user has ANY of the specified permissions.
        /// </summary>
        Task<bool> HasAnyPermissionAsync(Guid userId, IEnumerable<string> permissionKeys, CancellationToken ct = default);

        /// <summary>
        /// Check if user has ALL of the specified permissions.
        /// </summary>
        Task<bool> HasAllPermissionsAsync(Guid userId, IEnumerable<string> permissionKeys, CancellationToken ct = default);

        /// <summary>
        /// Get all users who have a specific permission.
        /// </summary>
        Task<IEnumerable<Guid>> GetUsersWithPermissionAsync(string permissionKey, CancellationToken ct = default);
    }
}
