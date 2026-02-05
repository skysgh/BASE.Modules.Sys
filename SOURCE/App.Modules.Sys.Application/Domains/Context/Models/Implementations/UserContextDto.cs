namespace App.Modules.Sys.Application.Domains.Context.Models.Implementations;

/// <summary>
/// User-level context information.
/// </summary>
public record UserContextDto
{
    /// <summary>
    /// User unique identifier.
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Whether the user is anonymous (not authenticated).
    /// </summary>
    public bool IsAnonymous { get; init; } = true;

    /// <summary>
    /// User display name.
    /// </summary>
    public string? DisplayName { get; init; }

    /// <summary>
    /// User email address.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// User profiles organized by domain/module.
    /// Extensible for future modules (education, health, demographics, etc.).
    /// </summary>
    public UserProfilesDto Profiles { get; init; } = new();

    /// <summary>
    /// User-specific notifications (unread counts, alerts).
    /// </summary>
    public UserNotificationsDto Notifications { get; init; } = new();
}

/// <summary>
/// Container for all user profiles organized by domain.
/// Extensible pattern for future modules.
/// </summary>
public record UserProfilesDto
{
    /// <summary>
    /// System profile (preferences, UI settings).
    /// </summary>
    public SystemProfileDto System { get; init; } = new();

    /// <summary>
    /// Security profile (roles, permissions, auth context).
    /// </summary>
    public SecurityProfileDto Security { get; init; } = new();

    // Future profiles can be added here:
    // public EducationProfileDto? Education { get; init; }
    // public HealthProfileDto? Health { get; init; }
    // public DemographicsProfileDto? Demographics { get; init; }
}

/// <summary>
/// System profile - user preferences and UI settings.
/// Managed by Sys module.
/// </summary>
public record SystemProfileDto
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

/// <summary>
/// Security profile - roles, permissions, and auth context.
/// Managed by Security module.
/// </summary>
public record SecurityProfileDto
{
    /// <summary>
    /// User roles.
    /// </summary>
    public List<string> Roles { get; init; } = new();

    /// <summary>
    /// User permissions (capability-based).
    /// </summary>
    public Dictionary<string, bool> Permissions { get; init; } = new();

    /// <summary>
    /// Security settings (2FA enabled, password expiry, etc.).
    /// </summary>
    public Dictionary<string, object> Settings { get; init; } = new();
}

/// <summary>
/// User notification summary.
/// </summary>
public record UserNotificationsDto
{
    /// <summary>
    /// Count of unread notifications.
    /// </summary>
    public int UnreadCount { get; init; }

    /// <summary>
    /// Count of pending tasks/approvals.
    /// </summary>
    public int PendingTasksCount { get; init; }

    /// <summary>
    /// Any critical alerts.
    /// </summary>
    public List<string> Alerts { get; init; } = new();
}

