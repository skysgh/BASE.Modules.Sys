using App.Modules.Sys.Shared.Models.Implementations;
using App.Modules.Sys.Shared.Services;

namespace App.Modules.Sys.Infrastructure.Services;

/// <summary>
/// Registry service for startup diagnostic logs.
/// Collects and stores startup log entries for diagnostics API.
/// Stateful singleton - maintains in-memory log collection.
/// Inherits from IHasRegistryService (which implies Singleton lifecycle - no need for additional attributes).
/// </summary>
public interface IStartupDiagnosticsRegistryService : IHasRegistryService
{
    /// <summary>
    /// Maximum number of log entries to retain.
    /// Default: 500. Prevents unbounded memory growth.
    /// </summary>
    int MaxEntries { get; set; }

    /// <summary>
    /// Gets all startup log entries in chronological order.
    /// </summary>
    IReadOnlyList<StartupLogEntry> GetAllEntries();

    /// <summary>
    /// Gets startup log entries filtered by tags.
    /// </summary>
    /// <param name="tags">Tags to filter by (OR logic).</param>
    IReadOnlyList<StartupLogEntry> GetEntriesByTags(params string[] tags);

    /// <summary>
    /// Begins a new startup log scope.
    /// Scope auto-finalizes and logs entry on dispose.
    /// RECOMMENDED: Use with 'using' statement for automatic cleanup.
    /// </summary>
    /// <param name="title">Entry title.</param>
    /// <param name="description">Optional description.</param>
    /// <param name="tags">Optional tags for categorization.</param>
    /// <returns>Disposable scope that finalizes on dispose.</returns>
    IStartupLogScope BeginScope(string title, string? description = null, params string[] tags);

    /// <summary>
    /// Manually adds a pre-built log entry (for advanced scenarios).
    /// Most code should use BeginScope() instead.
    /// </summary>
    void LogEntry(StartupLogEntry entry);

    /// <summary>
    /// Gets total startup duration (from first entry to last).
    /// </summary>
    TimeSpan GetTotalStartupDuration();

    /// <summary>
    /// Gets snapshot of current diagnostics state (domain object).
    /// </summary>
    StartupDiagnosticsSnapshot GetSnapshot();
}

/// <summary>
/// Disposable scope for automatic startup log entry finalization.
/// Automatically logs entry on dispose.
/// </summary>
public interface IStartupLogScope : IDisposable
{
    /// <summary>
    /// The underlying log entry being tracked.
    /// </summary>
    StartupLogEntry Entry { get; }

    /// <summary>
    /// Adds metadata to the log entry.
    /// </summary>
    void AddMetadata(string key, object value);

    /// <summary>
    /// Marks entry with an exception (auto-tagged as Error).
    /// </summary>
    void SetException(Exception ex);
}
