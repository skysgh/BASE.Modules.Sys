namespace App.Modules.Sys.Application.Domains.Diagnostics.Models;

/// <summary>
/// DTO for individual smoke test result.
/// Safe for API exposure - maps from Infrastructure.Diagnostics.SmokeTestResult.
/// </summary>
public record SmokeTestResultDto
{
    /// <summary>Test unique identifier (Key).</summary>
    public string TestId { get; init; } = string.Empty;
    
    /// <summary>Test status (Pass, Warning, Fail, Skipped, Pending).</summary>
    public string Status { get; init; } = "Pending";
    
    /// <summary>Human-readable test name.</summary>
    public string Title { get; init; } = string.Empty;
    
    /// <summary>Test description.</summary>
    public string Description { get; init; } = string.Empty;
    
    /// <summary>Category for grouping.</summary>
    public string Category { get; init; } = string.Empty;
    
    /// <summary>Test start time (UTC).</summary>
    public DateTime? StartUtc { get; init; }
    
    /// <summary>Test end time (UTC).</summary>
    public DateTime? EndUtc { get; init; }
    
    /// <summary>Computed duration.</summary>
    public TimeSpan Duration => (StartUtc != null && EndUtc != null) ? EndUtc.Value - StartUtc.Value : TimeSpan.Zero;
    
    /// <summary>Additional test metadata.</summary>
    public Dictionary<string, object> Details { get; init; } = new();
    
    /// <summary>Error message if test failed.</summary>
    public string? ErrorMessage { get; init; }
}

