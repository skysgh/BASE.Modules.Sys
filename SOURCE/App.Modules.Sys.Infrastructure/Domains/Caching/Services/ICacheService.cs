using App.Modules.Sys.Infrastructure.Services;
using App.Modules.Sys.Shared.Services;

namespace App.Modules.Sys.Infrastructure.Domains.Caching.Services
{
    /// <summary>
    /// Tier 2 Cache: Traditional key-value cache service.
    /// Can be in-memory, Redis, SQL Server, or any distributed cache.
    /// INTERNAL: Only accessible through ICacheObjectRegistry to enforce cache object pattern.
    /// </summary>
    internal interface IBackingCacheService : IHasService
    {
        /// <summary>
        /// Get value from cache
        /// </summary>
        Task<T?> GetAsync<T>(string key, CancellationToken ct = default);

        /// <summary>
        /// Set value in cache with optional expiry
        /// </summary>
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken ct = default);

        /// <summary>
        /// Remove value from cache
        /// </summary>
        Task RemoveAsync(string key, CancellationToken ct = default);

        /// <summary>
        /// Check if key exists in cache
        /// </summary>
        Task<bool> ExistsAsync(string key, CancellationToken ct = default);

        /// <summary>
        /// Get or create cache entry
        /// </summary>
        Task<T> GetOrCreateAsync<T>(
            string key, 
            Func<Task<T>> factory, 
            TimeSpan? expiry = null, 
            CancellationToken ct = default);
    }
}
