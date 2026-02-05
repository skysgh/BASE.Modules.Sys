using App.Modules.Sys.Application.Domains.Diagnostics.Models;
using App.Modules.Sys.Shared.Models.Implementations;
using App.Modules.Sys.Shared.ObjectMaps;

namespace App.Modules.Sys.Application.Domains.Diagnostics.Mapping;

/// <summary>
/// Maps Infrastructure domain object (StartupLogEntry) to Application DTO (StartupLogEntryDto).
/// Automatically discovered and registered with IObjectMappingService at startup.
/// </summary>
public class StartupLogEntryToEntryDtoMap : ObjectMapBase<StartupLogEntry, StartupLogEntryDto>
{
    /// <summary>
    /// Configure mapping rules.
    /// Custom mapping for enum→string and error message extraction.
    /// </summary>
    protected override void ConfigureMapping()
    {
        CreateMap()
            .MapFrom(dest => dest.Level, src => src.Level.ToString())
            .MapFrom(dest => dest.ErrorMessage, src => src.Exception != null ? src.Exception.Message : null);
    }
}

