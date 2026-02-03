using App.Modules.Sys.Shared.Services.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Application.Services.Workspace.CacheObjects
{
    /// <summary>
    /// Cache object for workspace IDs.
    /// Self-contained: knows how to load and refresh itself.
    /// Registered at startup for use by ICacheObjectRegistryService.
    /// </summary>
    public sealed class WorkspaceIdsCacheObject : ICacheObject
    {
        private HashSet<string>? _cachedValue;

        /// <inheritdoc/>
        public string Key => "Workspace.Ids";

        /// <inheritdoc/>
        public TimeSpan? Duration => TimeSpan.FromHours(24);

        /// <inheritdoc/>
        public Type ValueType => typeof(HashSet<string>);

        /// <inheritdoc/>
        public DateTime LastRefreshed { get; set; }

        /// <inheritdoc/>
        public bool IsExpired => 
            !Duration.HasValue ? false : 
            DateTime.UtcNow - LastRefreshed > Duration.Value;

        /// <inheritdoc/>
        public async Task RefreshAsync(CancellationToken ct = default)
        {
            // TODO: Load from repository when Workspace entity exists
            // For now, return hardcoded set
            await Task.Delay(10, ct);

            _cachedValue = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "default",
                "ibm",
                "acme",
                "demo"
            };

            LastRefreshed = DateTime.UtcNow;
        }

        /// <inheritdoc/>
        public object? GetValue() => _cachedValue;

        /// <inheritdoc/>
        public void Dispose()
        {
            // No resources to dispose
            _cachedValue = null;
        }
    }
}

