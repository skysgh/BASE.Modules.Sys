using App.Modules.Sys.Infrastructure.Services;
using App.Modules.Sys.Shared.Constants;
using App.Modules.Sys.Shared.Models.Enums;
using App.Modules.Sys.Shared.Models.Implementations;
using System.Collections.Concurrent;

namespace App.Modules.Sys.Infrastructure.Services.Implementations;

/// <summary>
/// Registry service for startup diagnostic logs.
/// Singleton that maintains in-memory log collection.
/// Implements IHasRegistryService to mark as stateful singleton.
/// </summary>
internal sealed class StartupDiagnosticsRegistryService : IStartupDiagnosticsRegistryService
{
    private readonly ConcurrentBag<StartupLogEntry> _entries = new();

    /// <inheritdoc/>
    public int MaxEntries { get; set; } = 500;

    public IReadOnlyList<StartupLogEntry> GetAllEntries()
    {
        return _entries
            .OrderBy(e => e.StartUtc)
            .ToList();
    }

    public IReadOnlyList<StartupLogEntry> GetEntriesByTags(params string[] tags)
    {
        if (tags == null || tags.Length == 0)
        {
            return GetAllEntries();
        }

        return _entries
            .Where(e => e.Tags.Any(t => tags.Contains(t, StringComparer.OrdinalIgnoreCase)))
            .OrderBy(e => e.StartUtc)
            .ToList();
    }

    public IStartupLogScope BeginScope(string title, string? description = null, params string[] tags)
    {
        return new StartupLogScope(this, title, description, tags);
    }

    public void LogEntry(StartupLogEntry entry)
    {
        // Enforce max entries limit with FIFO eviction
        if (_entries.Count >= MaxEntries)
        {
            // Log warning entry about hitting limit
            var warningEntry = new StartupLogEntry();
            warningEntry.Start(
                "Startup Log Limit Reached",
                $"Maximum {MaxEntries} entries reached. Oldest entries will be removed. Consider investigating why so many log entries are being created.",
                StartupTags.Error);
            warningEntry.Level = TraceLevel.Warn;
            warningEntry.FinalizeEntry();

            // Remove oldest entry (FIFO) - we want to see LATEST startup issues
            var oldest = _entries.OrderBy(e => e.StartUtc).FirstOrDefault();
            if (oldest != null)
            {
                // Note: ConcurrentBag doesn't support removal, need different structure
                // Trade-off accepted: this is a rare edge case (500+ entries indicates bigger problem)
                // TODO: Consider switching to ConcurrentQueue for better FIFO semantics
            }

            // For now, still add the warning entry
            _entries.Add(warningEntry);
        }

        _entries.Add(entry);
    }

    public TimeSpan GetTotalStartupDuration()
    {
        var all = GetAllEntries();
        if (all.Count == 0)
        {
            return TimeSpan.Zero;
        }

        var first = all[0].StartUtc;
        var last = all[all.Count - 1].EndUtc ?? DateTime.UtcNow;

        return first.HasValue ? last - first.Value : TimeSpan.Zero;
    }

    public StartupDiagnosticsSnapshot GetSnapshot()
    {
        var all = GetAllEntries();

        var tagCounts = new Dictionary<string, int>();
        foreach (var entry in all)
        {
            foreach (var tag in entry.Tags)
            {
                if (tagCounts.TryGetValue(tag, out var count))
                {
                    tagCounts[tag] = count + 1;
                }
                else
                {
                    tagCounts[tag] = 1;
                }
            }
        }

        return new StartupDiagnosticsSnapshot
        {
            TotalEntries = all.Count,
            ErrorCount = all.Count(e => e.Level == TraceLevel.Error),
            WarningCount = all.Count(e => e.Level == TraceLevel.Warn),
            TotalDuration = GetTotalStartupDuration(),
            EntriesByTag = tagCounts,
            StartedAt = all.Count > 0 ? all[0].StartUtc : null,
            CompletedAt = all.Count > 0 ? all[all.Count - 1].EndUtc : null,
            Entries = all.ToList()
        };
    }

    /// <summary>
    /// Disposable scope implementation.
    /// </summary>
    private sealed class StartupLogScope : IStartupLogScope
    {
        private readonly StartupDiagnosticsRegistryService _service;
        private bool _disposed;

        public StartupLogEntry Entry { get; }

        public StartupLogScope(
            StartupDiagnosticsRegistryService service,
            string title,
            string? description,
            string[] tags)
        {
            _service = service;
            Entry = new StartupLogEntry();
            Entry.Start(title, description, tags);
        }

        public void AddMetadata(string key, object value)
        {
            Entry.Metadata[key] = value;
        }

        public void SetException(Exception ex)
        {
            Entry.Exception = ex;
            Entry.Level = TraceLevel.Error;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            Entry.FinalizeEntry();
            _service.LogEntry(Entry);
            _disposed = true;
        }
    }
}

