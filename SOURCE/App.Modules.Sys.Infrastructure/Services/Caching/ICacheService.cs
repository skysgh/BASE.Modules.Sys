using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Infrastructure.Services.Caching
{
    /// <summary>
    /// Tier 2 Cache: Traditional key-value cache service.
    /// Can be in-memory, Redis, SQL Server, or any distributed cache.
    /// </summary>
    public interface ICacheService
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
