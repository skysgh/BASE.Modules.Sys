using App.Modules.Sys.Infrastructure.Diagnostics;
using App.Modules.Sys.Shared.Services.Caching;
using System.Globalization;

namespace App.Modules.Sys.Infrastructure.Caching.Examples
{
    /// <summary>
    /// Example: Active user count (refreshes frequently).
    /// Demonstrates circuit breaker in action - if DB is down, returns stale data.
    /// </summary>
    public class ActiveUserCountCacheObject : CacheObjectBase<int>
    {
        private readonly IAppLogger<ActiveUserCountCacheObject>? _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public ActiveUserCountCacheObject(IAppLogger<ActiveUserCountCacheObject>? logger = null)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public override string Key => "System.Metrics.ActiveUsers";

        /// <inheritdoc/>
        public override TimeSpan? Duration => TimeSpan.FromMinutes(1);

        /// <inheritdoc/>
        protected override async Task<int> GetValueAsync(CancellationToken ct = default)
        {
            _logger?.LogDebug("Querying active user count from database...");
            
            // Simulate database query
            await Task.Delay(50, ct);

            // In real implementation, query database:
            // return await _dbContext.Users.CountAsync(u => u.LastActivity > cutoff, ct);

            var count = Random.Shared.Next(100, 500); // Simulated count
            

            _logger?.LogDebug(
                string.Format(CultureInfo.InvariantCulture, $"Active user count: {count}", count));
            
            return count;
        }
    }
}

