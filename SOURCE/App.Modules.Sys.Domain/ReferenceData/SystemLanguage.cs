using System;
using System.ComponentModel.DataAnnotations;

namespace App.Modules.Sys.Domain.ReferenceData;

/// <summary>
/// System Language reference data.
/// Defines available UI languages for multi-language support.
/// </summary>
/// <remarks>
/// Reference Data Pattern:
/// - Read-heavy, write-rarely
/// - Cacheable
/// - Seed data managed via migrations
/// - Used by Context to populate computed settings
/// </remarks>
public class SystemLanguage
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// ISO 639-1 language code (e.g., "en", "es", "fr").
    /// Used for localization keys and URL routing.
    /// </summary>
    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = null!;

    /// <summary>
    /// Display name in English (e.g., "English", "Spanish", "French").
    /// Used for admin interfaces and documentation.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Display name in native language (e.g., "English", "Español", "Français").
    /// Used in language picker UI.
    /// </summary>
    [MaxLength(100)]
    public string? NativeName { get; set; }

    /// <summary>
    /// Optional description for admin purposes.
    /// </summary>
    [MaxLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// URL to flag/icon image (optional).
    /// Used in language picker UI.
    /// </summary>
    [MaxLength(500)]
    public string? IconUrl { get; set; }

    /// <summary>
    /// Whether this language is available for selection.
    /// Inactive languages don't appear in UI.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Whether this is the default/fallback language.
    /// Only one language should be default.
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// Display order in language picker (lower = first).
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// When this record was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// When this record was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Validation: Ensure code is valid ISO 639-1 format.
    /// </summary>
    public bool IsCodeValid()
    {
        return !string.IsNullOrWhiteSpace(Code) && Code.Length >= 2 && Code.Length <= 10;
    }

    /// <summary>
    /// Validation: Ensure at least one name is provided.
    /// </summary>
    public bool HasName()
    {
        return !string.IsNullOrWhiteSpace(Name) || !string.IsNullOrWhiteSpace(NativeName);
    }
}
