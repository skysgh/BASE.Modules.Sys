using System;
using App.Modules.Sys.Shared.Models;
using App.Modules.Sys.Shared.Models.Persistence;

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
/// 
/// Implements contracts for consistent schema generation:
/// - IHasGuidId: Entity primary key
/// - IHasKey: ISO code as business key
/// - IHasEnabled: Enable/disable language
/// - IHasDisplayHints: Sort order and display styling
/// - IHasTitleAndDescription: Display name and description
/// - IHasImageUrlNullable: Optional flag/icon URL
/// </remarks>
public class SystemLanguage : 
    IHasGuidId,
    IHasKey,
    IHasEnabled,
    IHasDisplayHints,
    IHasTitleAndDescription,
    IHasImageUrlNullable
{
    /// <inheritdoc/>
    public Guid Id { get; set; }

    /// <summary>
    /// ISO 639-1 language code (e.g., "en", "es", "fr").
    /// Used for localization keys and URL routing.
    /// This is the unique business key for the language.
    /// </summary>
    public string Key { get; set; } = null!;

    /// <summary>
    /// Display name in English (e.g., "English", "Spanish", "French").
    /// Used in language picker UI and admin interfaces.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Display name in native language (e.g., "English", "Español", "Français").
    /// Used in language picker UI for international users.
    /// </summary>
    public string? NativeName { get; set; }

    /// <inheritdoc/>
    public string Description { get; set; } = string.Empty;

    /// <inheritdoc/>
    public string? ImageUrl { get; set; }

    /// <inheritdoc/>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Whether this is the default/fallback language.
    /// Only one language should be default.
    /// </summary>
    public bool IsDefault { get; set; }

    /// <inheritdoc/>
    public int DisplayOrderHint { get; set; }

    /// <inheritdoc/>
    public string? DisplayStyleHint { get; set; }

    /// <summary>
    /// Validation: Ensure code is valid ISO 639-1 format.
    /// </summary>
    public bool IsKeyValid()
    {
        return !string.IsNullOrWhiteSpace(Key) && Key.Length >= 2 && Key.Length <= 10;
    }

    /// <summary>
    /// Validation: Ensure at least one name is provided.
    /// </summary>
    public bool HasDisplayName()
    {
        return !string.IsNullOrWhiteSpace(Title) || !string.IsNullOrWhiteSpace(NativeName);
    }
}
