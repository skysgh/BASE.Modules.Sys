using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Infrastructure.Services.Caching.Implementations
{
    /// <summary>
    /// In-memory implementation of ICacheRegistry.
    /// Tier 1 cache with refresh functions.
    /// Falls back to ICacheService (Tier 2) for persistence.
    /// </summary>
    public class CacheRegistryService : ICacheRegistry
    {
        private readonly ConcurrentDictionary<string, object> _cache = new();
        private readonly ICacheService? _backingCache;
        private readonly object _lock = new();

        /// <summary>
        /// Initializes a new instance of CacheRegistryService.
        /// </summary>
        /// <param name="backingCache">Optional Tier 2 cache for persistence</param>
        public CacheRegistryService(ICacheService? backingCache = null)
        {
            _backingCache = backingCache;
        }

        /// <inheritdoc/>
        public async Task<T?> GetOrCreateAsync<T>(
            string key, 
            Func<CancellationToken, Task<T>> factory, 
            TimeSpan? expiry = null, 
            CancellationToken ct = default)
        {
            // Check Tier 1 (in-memory registry)
            if (_cache.TryGetValue(key, out var cached) && cached is CacheEntry<T> entry)
            {
                if (!entry.IsExpired)
                {
                    return entry.Value;
                }

                // Expired - refresh
                await entry.RefreshAsync(ct);
                return entry.Value;
            }

            // Check Tier 2 (backing cache) if available
            if (_backingCache != null)
            {
                var backingValue = await _backingCache.GetAsync<T>(key, ct);
                if (backingValue != null)
                {
                    SetValue(key, backingValue, expiry, factory);
                    return backingValue;
                }
            }

            // Not in cache - create new
            var value = await factory(ct);
            SetValue(key, value, expiry, factory);


            // Store in backing cache
            if (_backingCache != null && value != null)
            {
                await _backingCache.SetAsync(key, value, expiry, ct);
            }

            return value;
        }

        /// <inheritdoc/>
        public T? GetValue<T>(string key)
        {
            if (_cache.TryGetValue(key, out var cached) && cached is CacheEntry<T> entry)
            {
                return entry.IsExpired ? default : entry.Value;
            }
            return default;
        }

        /// <inheritdoc/>
        public void SetValue<T>(
            string key, 
            T value, 
            TimeSpan? expiry = null, 
            Func<CancellationToken, Task<T>>? refreshFunction = null)
        {
            var entry = new CacheEntry<T>
            {
                Value = value,
                LastRefreshed = DateTime.UtcNow,
                ExpiresIn = expiry,
                RefreshFunction = refreshFunction
            };

            _cache[key] = entry;
        }

        /// <inheritdoc/>
        public void Remove(string key)
        {
            _cache.TryRemove(key, out _);
        }

        /// <inheritdoc/>
        public int RemoveByPattern(string pattern)
        {
            var regex = new Regex(
                "^" + Regex.Escape(pattern).Replace("\\*", ".*") + "$",
                RegexOptions.IgnoreCase);

            var keysToRemove = _cache.Keys.Where(k => regex.IsMatch(k)).ToList();

            foreach (var key in keysToRemove)
            {
                _cache.TryRemove(key, out _);
            }

            return keysToRemove.Count;
        }

        /// <inheritdoc/>
        public async Task RefreshAsync(string key, CancellationToken ct = default)
        {
            if (_cache.TryGetValue(key, out var cached))
            {
                var entryType = cached.GetType();
                if (entryType.IsGenericType && 
                    entryType.GetGenericTypeDefinition() == typeof(CacheEntry<>))
                {
                    var refreshMethod = entryType.GetMethod(nameof(CacheEntry<object>.RefreshAsync));
                    if (refreshMethod != null)
                    {
                        await (Task)refreshMethod.Invoke(cached, new object[] { ct })!;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public async Task RefreshExpiredAsync(CancellationToken ct = default)
        {
            var tasks = new List<Task>();

            foreach (var kvp in _cache)
            {
                var entryType = kvp.Value.GetType();
                if (entryType.IsGenericType && 
                    entryType.GetGenericTypeDefinition() == typeof(CacheEntry<>))
                {
                    var isExpiredProperty = entryType.GetProperty(nameof(CacheEntry<object>.IsExpired));
                    if (isExpiredProperty != null && (bool)isExpiredProperty.GetValue(kvp.Value)!)
                    {
                        tasks.Add(RefreshAsync(kvp.Key, ct));
                    }
                }
            }

            await Task.WhenAll(tasks);
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetKeys() => _cache.Keys;

        /// <inheritdoc/>
        public void Clear() => _cache.Clear();
    }
}
