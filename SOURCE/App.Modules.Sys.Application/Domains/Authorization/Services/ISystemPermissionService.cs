using App.Modules.Sys.Interfaces.Models.Authorization;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Application.Domains.Authorization.Services
{
    /// <summary>
    /// Service for querying system permissions.
    /// Read-only - permissions defined in ReferenceDataSeeder.
    /// </summary>
    public interface ISystemPermissionService
    {
        /// <summary>
        /// Get all system permissions, optionally filtered by category.
        /// </summary>
        Task<IEnumerable<SystemPermissionDto>> GetPermissionsAsync(
            string? category = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get permission by key.
        /// </summary>
        Task<SystemPermissionDto?> GetPermissionByKeyAsync(
            string key,
            CancellationToken ct = default);

        /// <summary>
        /// Get distinct permission categories.
        /// </summary>
        Task<IEnumerable<string>> GetCategoriesAsync(
            CancellationToken ct = default);
    }
}

