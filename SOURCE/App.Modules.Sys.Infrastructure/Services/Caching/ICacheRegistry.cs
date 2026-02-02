using App.Modules.Sys.Shared.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Infrastructure.Services.Caching
{
    /// <summary>
    /// Tier 1 Cache: Registry of active cache entries with refresh functions.
    /// Manages in-memory cache objects that can refresh themselves.
    /// Falls back to Tier 2 (ICacheService) for distributed/persistent caching.
    /// </summary>
    public interface ICacheRegistry : IHasRegistryService
    {
        /// <summary>
        /// Get cached value or create it using the factory function.
        /// Automatically refreshes if expired.
        /// </summary>
        Task<T?> GetOrCreateAsync<T>(
            string key, 
            Func<CancellationToken, Task<T>> factory, 
            TimeSpan? expiry = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get cached value without refresh (returns null if expired)
        /// </summary>
        T? GetValue<T>(string key);

        /// <summary>
        /// Manually set a cache entry with optional refresh function
        /// </summary>
        void SetValue<T>(
            string key, 
            T value, 
            TimeSpan? expiry = null, 
            Func<CancellationToken, Task<T>>? refreshFunction = null);

        /// <summary>
        /// Remove entry from cache
        /// </summary>
        void Remove(string key);

        /// <summary>
        /// Remove all entries matching pattern (e.g., "workspace:123:*")
        /// </summary>
        int RemoveByPattern(string pattern);

        /// <summary>
        /// Manually refresh a specific cache entry
        /// </summary>
        Task RefreshAsync(string key, CancellationToken ct = default);

        /// <summary>
        /// Refresh all expired entries
        /// </summary>
        Task RefreshExpiredAsync(CancellationToken ct = default);

        /// <summary>
        /// Get all registered cache keys
        /// </summary>
        IEnumerable<string> GetKeys();

        /// <summary>
        /// Clear all cache entries
        /// </summary>
        void Clear();
    }
}
