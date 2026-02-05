using System;

namespace App.Modules.Sys.Application.ReferenceData.Models;

/// <summary>
/// DTO for SystemLanguage reference data.
/// Used for API responses and frontend consumption.
/// </summary>
public record SystemLanguageDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// ISO 639-1 language code (e.g., "en", "es", "fr").
    /// </summary>
    public string Code { get; init; } = null!;

    /// <summary>
    /// Display name in English (e.g., "English", "Spanish").
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Display name in native language (e.g., "English", "Español", "Français").
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
    public bool IsActive { get; init; }

    /// <summary>
    /// Whether this is the default language.
    /// </summary>
    public bool IsDefault { get; init; }

    /// <summary>
    /// Display order in UI.
    /// </summary>
    public int SortOrder { get; init; }
}
