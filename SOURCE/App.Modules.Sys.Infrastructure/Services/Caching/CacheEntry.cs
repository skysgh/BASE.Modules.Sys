using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Infrastructure.Services.Caching
{
    /// <summary>
    /// A cache entry that can refresh itself when expired.
    /// Stores both the value and the function to regenerate it.
    /// </summary>
    /// <typeparam name="T">Type of cached value</typeparam>
    public class CacheEntry<T>
    {
        /// <summary>
        /// The cached value
        /// </summary>
        public T? Value { get; set; }

        /// <summary>
        /// When this entry was last refreshed
        /// </summary>
        public DateTime LastRefreshed { get; set; }

        /// <summary>
        /// How long until this entry expires (null = never expires)
        /// </summary>
        public TimeSpan? ExpiresIn { get; set; }

        /// <summary>
        /// Function to call to refresh the value when expired
        /// </summary>
        public Func<CancellationToken, Task<T>>? RefreshFunction { get; set; }

        /// <summary>
        /// Whether this entry has expired
        /// </summary>
        public bool IsExpired => ExpiresIn.HasValue && 
                                 DateTime.UtcNow > LastRefreshed.Add(ExpiresIn.Value);

        /// <summary>
        /// Refresh the cached value using the refresh function
        /// </summary>
        public async Task RefreshAsync(CancellationToken ct = default)
        {
            if (RefreshFunction != null)
            {
                Value = await RefreshFunction(ct);
                LastRefreshed = DateTime.UtcNow;
            }
        }
    }
}
