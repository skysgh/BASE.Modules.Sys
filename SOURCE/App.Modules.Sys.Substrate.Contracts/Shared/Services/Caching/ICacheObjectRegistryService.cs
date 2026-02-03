using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Shared.Services.Caching
{
    /// <summary>
    /// Registry service for self-contained cache objects.
    /// Discovers cache objects via reflection at startup and manages their lifecycle.
    /// </summary>
    public interface ICacheObjectRegistryService : IHasRegistryService
    {
        /// <summary>
        /// Register a cache object in the registry.
        /// Typically called automatically during startup via reflection.
        /// </summary>
        /// <param name="cacheObject">The cache object to register</param>
        void Register(ICacheObject cacheObject);

        /// <summary>
        /// Get a cached value by key.
        /// Automatically refreshes if expired.
        /// </summary>
        /// <typeparam name="T">Type of the cached value</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>The cached value, or default if not found</returns>
        Task<T?> GetValueAsync<T>(string key, CancellationToken ct = default);

        /// <summary>
        /// Force refresh a specific cache object by key.
        /// </summary>
        /// <param name="key">Cache key to refresh</param>
        /// <param name="ct">Cancellation token</param>
        Task RefreshAsync(string key, CancellationToken ct = default);

        /// <summary>
        /// Refresh all expired cache objects.
        /// Useful for background refresh tasks.
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task RefreshExpiredAsync(CancellationToken ct = default);

        /// <summary>
        /// Get all registered cache keys.
        /// </summary>
        /// <returns>Collection of all cache keys</returns>
        IEnumerable<string> GetKeys();

        /// <summary>
        /// Check if a cache object exists for the given key.
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <returns>True if registered</returns>
        bool Contains(string key);

        /// <summary>
        /// Remove a cache object from the registry.
        /// </summary>
        /// <param name="key">Cache key to remove</param>
        void Remove(string key);

        /// <summary>
        /// Clear all cache objects from the registry.
        /// </summary>
        void Clear();
    }
}
