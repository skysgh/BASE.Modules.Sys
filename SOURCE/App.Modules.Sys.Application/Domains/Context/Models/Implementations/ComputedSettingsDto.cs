namespace App.Modules.Sys.Application.Domains.Context.Models.Implementations;

/// <summary>
/// Computed/merged settings (user > workspace > system).
/// This represents the effective settings after hierarchy resolution.
/// </summary>
public record ComputedSettingsDto
{
    /// <summary>
    /// Effective language/locale.
    /// </summary>
    public string Language { get; init; } = "en-US";

    /// <summary>
    /// Effective timezone.
    /// </summary>
    public string Timezone { get; init; } = "UTC";

    /// <summary>
    /// Effective theme.
    /// </summary>
    public string Theme { get; init; } = "Auto";

    /// <summary>
    /// Effective date format.
    /// </summary>
    public string DateFormat { get; init; } = "MM/DD/YYYY";

    /// <summary>
    /// Effective time format.
    /// </summary>
    public string TimeFormat { get; init; } = "12h";

    /// <summary>
    /// Effective branding (merged from system, workspace, user).
    /// </summary>
    public EffectiveBrandingDto Branding { get; init; } = new();

    /// <summary>
    /// Additional merged settings.
    /// </summary>
    public Dictionary<string, object> AdditionalSettings { get; init; } = new();
}

/// <summary>
/// Effective branding after hierarchy resolution.
/// </summary>
public record EffectiveBrandingDto
{
    /// <summary>
    /// Effective organization/platform name.
    /// </summary>
    public string Name { get; init; } = "BASE Platform";

    /// <summary>
    /// Effective logo URL.
    /// </summary>
    public string? LogoUrl { get; init; }

    /// <summary>
    /// Effective primary color.
    /// </summary>
    public string? PrimaryColor { get; init; }

    /// <summary>
    /// Effective custom CSS URL.
    /// </summary>
    public string? CustomCssUrl { get; init; }
}
