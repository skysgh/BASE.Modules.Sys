namespace App.Modules.Sys.Application.Domains.Context.Models.Implementations;

/// <summary>
/// System-level context information (platform/provider level).
/// This represents the BASE platform itself, not tenant-specific data.
/// </summary>
public record SystemContextDto
{
    /// <summary>
    /// Platform name (e.g., "BASE Platform").
    /// </summary>
    public string Name { get; init; } = "BASE Platform";

    /// <summary>
    /// Platform version.
    /// </summary>
    public string Version { get; init; } = "1.0.0";

    /// <summary>
    /// Environment name (Development, Staging, Production).
    /// </summary>
    public string Environment { get; init; } = "Development";

    /// <summary>
    /// System-level branding (platform provider information).
    /// </summary>
    public SystemBrandingDto Branding { get; init; } = new();

    /// <summary>
    /// Global feature flags (platform-wide capabilities).
    /// </summary>
    public Dictionary<string, bool> Features { get; init; } = new();
}

/// <summary>
/// Platform provider branding information.
/// </summary>
public record SystemBrandingDto
{
    /// <summary>
    /// Provider information (company that operates the platform).
    /// </summary>
    public ProviderInfoDto Provider { get; init; } = new();

    /// <summary>
    /// Developer contact information.
    /// </summary>
    public DeveloperInfoDto Developer { get; init; } = new();
}

/// <summary>
/// Platform provider information.
/// </summary>
public record ProviderInfoDto
{
    /// <summary>
    /// Provider/company name.
    /// </summary>
    public string Name { get; init; } = "Your Company LLC";
    
    /// <summary>
    /// Provider contact email.
    /// </summary>
    public string? ContactEmail { get; init; }
    
    /// <summary>
    /// Provider logo URL.
    /// </summary>
    public string? LogoUrl { get; init; }
    
    /// <summary>
    /// Support portal URL.
    /// </summary>
    public string? SupportUrl { get; init; }
    
    /// <summary>
    /// Legal/terms URL.
    /// </summary>
    public string? LegalUrl { get; init; }
}

/// <summary>
/// Developer contact information.
/// </summary>
public record DeveloperInfoDto
{
    /// <summary>
    /// Developer team name.
    /// </summary>
    public string Name { get; init; } = "Development Team";
    
    /// <summary>
    /// Developer contact email.
    /// </summary>
    public string? ContactEmail { get; init; }
    
    /// <summary>
    /// Developer email (alternative).
    /// </summary>
    public string? Email { get; init; }
    
    /// <summary>
    /// Documentation URL.
    /// </summary>
    public string? DocsUrl { get; init; }
}
