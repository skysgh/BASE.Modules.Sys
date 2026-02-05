using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Application.Domains.Workspace.Services
{
    /// <summary>
    /// Interface for cache management operations.
    /// Allows refreshing workspace cache when workspaces are added/removed.
    /// </summary>
    public interface IWorkspaceCacheManager
    {
        /// <summary>
        /// Refresh workspace cache from database.
        /// Call this when workspaces are created, updated, or deleted.
        /// </summary>
        Task RefreshCacheAsync(CancellationToken ct = default);
    }
}
