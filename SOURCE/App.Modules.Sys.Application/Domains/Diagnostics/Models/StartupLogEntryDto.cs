namespace App.Modules.Sys.Application.Domains.Diagnostics.Models;

/// <summary>
/// DTO for startup log entry.
/// </summary>
public record StartupLogEntryDto
{
    /// <summary>Entry title.</summary>
    public string Title { get; init; } = string.Empty;
    
    /// <summary>Entry description.</summary>
    public string Description { get; init; } = string.Empty;
    
    /// <summary>Log level (Debug, Info, Warn, Error).</summary>
    public string Level { get; init; } = "Info";
    
    /// <summary>Tags for categorization.</summary>
    public List<string> Tags { get; init; } = new();
    
    /// <summary>Operation start time (UTC).</summary>
    public DateTime? StartUtc { get; init; }
    
    /// <summary>Operation end time (UTC).</summary>
    public DateTime? EndUtc { get; init; }
    
    /// <summary>Computed duration.</summary>
    public TimeSpan Duration => (StartUtc != null && EndUtc != null) ? EndUtc.Value - StartUtc.Value : TimeSpan.Zero;
    
    /// <summary>Additional metadata.</summary>
    public Dictionary<string, object> Metadata { get; init; } = new();
    
    /// <summary>Error message if operation failed.</summary>
    public string? ErrorMessage { get; init; }
}

