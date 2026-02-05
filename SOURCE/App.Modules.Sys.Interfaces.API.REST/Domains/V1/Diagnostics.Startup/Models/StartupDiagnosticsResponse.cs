using App.Modules.Sys.Application.Domains.Diagnostics.Models;

namespace App.Modules.Sys.Interfaces.Domains.V1.Diagnostics.Startup.Models;

/// <summary>
/// Complete startup diagnostics response.
/// Returns DTOs (not domain objects) for API consumption.
/// </summary>
public record StartupDiagnosticsResponse
{
    /// <summary>
    /// Summary statistics.
    /// </summary>
    public StartupDiagnosticsSummaryDto Summary { get; init; } = null!;
    
    /// <summary>
    /// Full list of log entries.
    /// </summary>
    public IReadOnlyList<StartupLogEntryDto> Entries { get; init; } = Array.Empty<StartupLogEntryDto>();
}

