using App.Modules.Sys.Application.Domains.Diagnostics.Models;
using App.Modules.Sys.Infrastructure.Domains.Diagnostics.Implementations;
using App.Modules.Sys.Shared.ObjectMaps;

namespace App.Modules.Sys.Application.Domains.Diagnostics.Mapping;

/// <summary>
/// Maps Infrastructure domain object (SmokeTestResult) to Application DTO (SmokeTestResultDto).
/// Automatically discovered and registered with IObjectMappingService at startup.
/// </summary>
public class SmokeTestResultToResultDtoMap : ObjectMapBase<SmokeTestResult, SmokeTestResultDto>
{
    /// <summary>
    /// Configure mapping rules.
    /// Convention handles most properties. Custom mapping for enum→string and error handling.
    /// </summary>
    protected override void ConfigureMapping()
    {
        CreateMap()
            .MapFrom(dest => dest.Status, src => src.Status.ToString())
            .MapFrom(dest => dest.ErrorMessage, src => src.Exception != null ? src.Exception.Message : null);
    }
}

