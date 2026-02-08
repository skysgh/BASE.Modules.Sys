namespace App.Modules.Sys.Application.ReferenceData.Models;

/// <summary>
/// DTO for creating a new SystemLanguage.
/// </summary>
public record CreateSystemLanguageDto
{
    /// <summary>
    /// ISO 639-1 language code (e.g., "en", "es", "fr").
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// Display name in English (e.g., "English", "Spanish").
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Display name in native language (e.g., "English", "Espanol", "Francais").
    /// </summary>
    public string? NativeName { get; init; }

    /// <summary>
    /// Optional description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// URL to flag/icon image.
    /// </summary>
    public string? IconUrl { get; init; }

    /// <summary>
    /// Whether this language is available for selection.
    /// </summary>
    public bool IsActive { get; init; } = true;

    /// <summary>
    /// Whether this is the default language.
    /// </summary>
    public bool IsDefault { get; init; }

    /// <summary>
    /// Display order in UI.
    /// </summary>
    public int SortOrder { get; init; }
}

/// <summary>
/// DTO for updating an existing SystemLanguage.
/// </summary>
public record UpdateSystemLanguageDto
{
    /// <summary>
    /// Display name in English (e.g., "English", "Spanish").
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Display name in native language (e.g., "English", "Espanol", "Francais").
    /// </summary>
    public string? NativeName { get; init; }

    /// <summary>
    /// Optional description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// URL to flag/icon image.
    /// </summary>
    public string? IconUrl { get; init; }

    /// <summary>
    /// Whether this language is available for selection.
    /// </summary>
    public bool? IsActive { get; init; }

    /// <summary>
    /// Whether this is the default language.
    /// </summary>
    public bool? IsDefault { get; init; }

    /// <summary>
    /// Display order in UI.
    /// </summary>
    public int? SortOrder { get; init; }
}
