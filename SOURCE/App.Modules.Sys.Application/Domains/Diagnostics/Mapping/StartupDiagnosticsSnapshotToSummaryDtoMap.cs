using App.Modules.Sys.Application.Domains.Diagnostics.Models;
using App.Modules.Sys.Infrastructure.Services;
using App.Modules.Sys.Shared.ObjectMaps;

namespace App.Modules.Sys.Application.Domains.Diagnostics.Mapping;

/// <summary>
/// Maps Infrastructure domain object (StartupDiagnosticsSnapshot) to Application DTO (StartupDiagnosticsSummaryDto).
/// Automatically discovered and registered with IObjectMappingService at startup.
/// </summary>
public class StartupDiagnosticsSnapshotToSummaryDtoMap : ObjectMapBase<StartupDiagnosticsSnapshot, StartupDiagnosticsSummaryDto>
{
    /// <summary>
    /// Configure mapping rules.
    /// Convention-based mapping handles all properties with matching names.
    /// Entries collection will auto-map using StartupLogEntry → StartupLogEntryDto mapper.
    /// </summary>
    protected override void ConfigureMapping()
    {
        // Pure convention-based mapping - all properties match by name
        // Entries collection will use StartupLogEntry → StartupLogEntryDto mapper automatically
    }
}

