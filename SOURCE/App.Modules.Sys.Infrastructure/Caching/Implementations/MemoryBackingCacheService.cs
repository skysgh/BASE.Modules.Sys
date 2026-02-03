using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Infrastructure.Caching.Implementations
{
    /// <summary>
    /// Default in-memory implementation of ICacheService (Tier 2).
    /// Uses framework's IMemoryCache for storage.
    /// Can be replaced with Redis/SQL implementation via DI when Azure assemblies detected.
    /// INTERNAL: Only accessible through ICacheObjectRegistry.
    /// </summary>
    internal sealed class MemoryBackingCacheService : IBackingCacheService
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryBackingCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
        {
            var value = _memoryCache.TryGetValue(key, out T? result) ? result : default;
            return Task.FromResult(value);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken ct = default)
        {
            var options = new MemoryCacheEntryOptions();
            
            if (expiry.HasValue)
            {
                options.AbsoluteExpirationRelativeToNow = expiry;
            }
            
            _memoryCache.Set(key, value, options);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key, CancellationToken ct = default)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(string key, CancellationToken ct = default)
        {
            var exists = _memoryCache.TryGetValue(key, out _);
            return Task.FromResult(exists);
        }

        public async Task<T> GetOrCreateAsync<T>(
            string key, 
            Func<Task<T>> factory, 
            TimeSpan? expiry = null, 
            CancellationToken ct = default)
        {
            // Try get existing
            if (_memoryCache.TryGetValue(key, out T? existingValue))
            {
                return existingValue!;
            }

            // Create new
            var newValue = await factory();
            
            // Store with expiry
            var options = new MemoryCacheEntryOptions();
            if (expiry.HasValue)
            {
                options.AbsoluteExpirationRelativeToNow = expiry;
            }
            
            _memoryCache.Set(key, newValue, options);
            
            return newValue;
        }
    }
}
