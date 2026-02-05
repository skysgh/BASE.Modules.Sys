using App.Modules.Sys.Application.Domains.Diagnostics.Models;
using App.Modules.Sys.Infrastructure.Domains.Diagnostics;
using App.Modules.Sys.Shared.ObjectMaps;

namespace App.Modules.Sys.Application.Domains.Diagnostics.Mapping;

/// <summary>
/// Maps Infrastructure domain object (SmokeTestExecutionResult) to Application DTO (SmokeTestSummaryDto).
/// Automatically discovered and registered with IObjectMappingService at startup.
/// </summary>
public class SmokeTestExecutionResultToSummaryDtoMap : ObjectMapBase<SmokeTestExecutionResult, SmokeTestSummaryDto>
{
    /// <summary>
    /// Configure mapping rules.
    /// Uses convention-based mapping for properties with matching names.
    /// Custom mappings for calculated/transformed properties.
    /// </summary>
    protected override void ConfigureMapping()
    {
        CreateMap()
            .MapFrom(dest => dest.OverallStatus, src => DetermineOverallStatus(src))
            .MapFrom(dest => dest.LastRunAt, src => src.ExecutedAt);
        
        // Note: Results collection will auto-map using SmokeTestResult → SmokeTestResultDto mapper
    }

    /// <summary>
    /// Determine overall health status from execution result.
    /// </summary>
    private static string DetermineOverallStatus(SmokeTestExecutionResult result)
    {
        if (result.CriticalFailures > 0)
        {
            return "Unhealthy";
        }
        
        if (result.FailedTests > 0 || result.WarningTests > 0)
        {
            return "Degraded";
        }
        
        return result.TotalTests > 0 ? "Healthy" : "Unknown";
    }
}

