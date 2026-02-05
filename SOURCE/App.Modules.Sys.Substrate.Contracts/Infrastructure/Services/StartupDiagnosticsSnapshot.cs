using App.Modules.Sys.Shared.Models.Implementations;

namespace App.Modules.Sys.Infrastructure.Services;

/// <summary>
/// Domain object representing startup diagnostics snapshot.
/// Used within Infrastructure layer only - NOT exposed via API.
/// Application layer maps this to DTOs for external consumption.
/// </summary>
public class StartupDiagnosticsSnapshot
{
    /// <summary>
    /// Total number of log entries.
    /// </summary>
    public int TotalEntries { get; set; }

    /// <summary>
    /// Number of error-level entries.
    /// </summary>
    public int ErrorCount { get; set; }

    /// <summary>
    /// Number of warning-level entries.
    /// </summary>
    public int WarningCount { get; set; }

    /// <summary>
    /// Total duration from first to last entry.
    /// </summary>
    public TimeSpan TotalDuration { get; set; }

    /// <summary>
    /// Count of entries grouped by tag.
    /// </summary>
    public Dictionary<string, int> EntriesByTag { get; set; } = new();

    /// <summary>
    /// When startup began (first entry).
    /// </summary>
    public DateTime? StartedAt { get; set; }

    /// <summary>
    /// When startup completed (last entry).
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// All log entries.
    /// </summary>
    public List<StartupLogEntry> Entries { get; set; } = new();
}
