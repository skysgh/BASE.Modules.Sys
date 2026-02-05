namespace App.Modules.Sys.Application.Domains.Context.Models.Implementations;

/// <summary>
/// User profile and preferences.
/// </summary>
public record UserProfileDto
{
    /// <summary>
    /// User's preferred language/locale (e.g., "en-US", "fr-FR").
    /// </summary>
    public string Language { get; init; } = "en-US";

    /// <summary>
    /// User's preferred timezone (e.g., "America/New_York").
    /// </summary>
    public string Timezone { get; init; } = "UTC";

    /// <summary>
    /// User's preferred theme (Light, Dark, Auto).
    /// </summary>
    public string Theme { get; init; } = "Auto";

    /// <summary>
    /// User's preferred date format.
    /// </summary>
    public string DateFormat { get; init; } = "MM/DD/YYYY";

    /// <summary>
    /// User's preferred time format (12h, 24h).
    /// </summary>
    public string TimeFormat { get; init; } = "12h";

    /// <summary>
    /// Whether user wants email notifications.
    /// </summary>
    public bool EmailNotifications { get; init; } = true;

    /// <summary>
    /// Whether user wants in-app notifications.
    /// </summary>
    public bool InAppNotifications { get; init; } = true;

    /// <summary>
    /// Avatar/profile picture URL.
    /// </summary>
    public string? AvatarUrl { get; init; }

    /// <summary>
    /// Additional custom preferences (key-value pairs).
    /// </summary>
    public Dictionary<string, object> CustomPreferences { get; init; } = new();
}
