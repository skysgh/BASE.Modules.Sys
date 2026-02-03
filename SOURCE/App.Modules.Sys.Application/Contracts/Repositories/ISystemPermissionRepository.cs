using App.Modules.Sys.Domain.Authorization;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Application.Contracts.Repositories
{
    /// <summary>
    /// Repository interface for SystemPermission aggregate.
    /// Application layer depends on this interface, not implementation.
    /// </summary>
    public interface ISystemPermissionRepository
    {
        /// <summary>
        /// Get all permissions, optionally filtered by category.
        /// </summary>
        Task<IEnumerable<SystemPermission>> GetAllAsync(
            string? category = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get permission by key.
        /// </summary>
        Task<SystemPermission?> GetByKeyAsync(
            string key,
            CancellationToken ct = default);

        /// <summary>
        /// Get distinct categories.
        /// </summary>
        Task<IEnumerable<string>> GetCategoriesAsync(
            CancellationToken ct = default);
    }
}
