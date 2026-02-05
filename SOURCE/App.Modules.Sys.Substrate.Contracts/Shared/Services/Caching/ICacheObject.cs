using App.Modules.Sys.Shared.Lifecycles;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Shared.Services.Caching
{
    /// <summary>
    /// Contract for self-contained cache objects that know how to refresh themselves.
    /// Discovered via reflection at startup and registered in the cache object registry.
    /// </summary>
    public interface ICacheObject : IHasSingletonLifecycle, IDisposable
    {
        /// <summary>
        /// Unique key identifying this cached object.
        /// Must be unique across the entire application.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// How long the cached value remains valid before requiring refresh.
        /// Null means never expires (static/permanent cache).
        /// </summary>
        TimeSpan? Duration { get; }

        /// <summary>
        /// Type of the cached value.
        /// Used for type-safe retrieval.
        /// </summary>
        Type ValueType { get; }

        /// <summary>
        /// When this cache object was last refreshed.
        /// Updated automatically by the registry after refresh.
        /// </summary>
        DateTime LastRefreshed { get; set; }

        /// <summary>
        /// Whether this cache object has expired and needs refresh.
        /// </summary>
        bool IsExpired { get; }

        /// <summary>
        /// Refresh the cached value.
        /// Called automatically by the registry when expired.
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Task completing when refresh is done</returns>
        Task RefreshAsync(CancellationToken ct = default);

        /// <summary>
        /// Get the cached value as an object.
        /// Use the generic GetValue method on the registry for type-safe access.
        /// </summary>
        /// <returns>The cached value</returns>
        object? GetValue();
    }
}
