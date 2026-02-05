namespace App.Modules.Sys.Application.Domains.Diagnostics.Models;

/// <summary>
/// DTO for startup diagnostics summary.
/// Safe for API exposure - maps from Infrastructure.Services.StartupDiagnosticsSnapshot.
/// </summary>
public record StartupDiagnosticsSummaryDto
{
    /// <summary>
    /// Total number of log entries.
    /// </summary>
    public int TotalEntries { get; init; }

    /// <summary>
    /// Number of error-level entries.
    /// </summary>
    public int ErrorCount { get; init; }

    /// <summary>
    /// Number of warning-level entries.
    /// </summary>
    public int WarningCount { get; init; }

    /// <summary>
    /// Total startup duration (from first to last entry).
    /// </summary>
    public TimeSpan TotalDuration { get; init; }

    /// <summary>
    /// Entry counts grouped by tag.
    /// Example: {"Module": 3, "Service": 42, "DbContext": 2}
    /// </summary>
    public Dictionary<string, int> EntriesByTag { get; init; } = new();

    /// <summary>
    /// When startup began (UTC).
    /// </summary>
    public DateTime? StartedAt { get; init; }

    /// <summary>
    /// When startup completed (UTC).
    /// </summary>
    public DateTime? CompletedAt { get; init; }

    /// <summary>
    /// Individual log entries (optional - can be excluded for summary-only response).
    /// </summary>
    public List<StartupLogEntryDto>? Entries { get; init; }
}
