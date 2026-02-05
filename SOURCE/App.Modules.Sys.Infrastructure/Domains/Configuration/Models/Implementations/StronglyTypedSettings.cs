namespace App.Modules.Sys.Infrastructure.Domains.Configuration.Models.Implementations
{
    /// <summary>
    /// Strongly-typed settings for System/Security section.
    /// Maps to Settings:System:Security in appsettings.json
    /// </summary>
    /// <remarks>
    /// Usage:
    /// <code>
    /// // Option 1: Via ISettingsService (with hierarchy)
    /// var security = await settingsService.GetSectionAsync&lt;SecuritySettings&gt;(
    ///     "Settings/System/Security", workspaceId, userId);
    /// 
    /// // Option 2: Via IOptions (standard Microsoft pattern)
    /// services.AddSettingsAsOptions&lt;SecuritySettings&gt;("Settings:System:Security");
    /// public MyService(IOptions&lt;SecuritySettings&gt; options) { }
    /// </code>
    /// </remarks>
    public class SecuritySettings
    {
        public TwoFactorAuthSettings TwoFactorAuth { get; set; } = new();
        public PasswordPolicySettings PasswordPolicy { get; set; } = new();
    }

    public class TwoFactorAuthSettings
    {
        public bool Enabled { get; set; }
    }

    public class PasswordPolicySettings
    {
        public int MinLength { get; set; } = 8;
        public bool RequireSpecialChar { get; set; } = true;
    }

    /// <summary>
    /// Strongly-typed settings for App/Preferences section.
    /// Maps to Settings:App:Preferences in appsettings.json
    /// </summary>
    public class PreferencesSettings
    {
        public string Theme { get; set; } = "Light";
        public string Language { get; set; } = "en-US";
        public string Timezone { get; set; } = "UTC";
        public string DateFormat { get; set; } = "yyyy-MM-dd";
        public BackgroundSettings Background { get; set; } = new();
    }

    public class BackgroundSettings
    {
        public string Color { get; set; } = "#FFFFFF";
        public double Opacity { get; set; } = 1.0;
    }

    /// <summary>
    /// Strongly-typed settings for System/Identity section.
    /// Maps to Settings:System:Identity in appsettings.json
    /// </summary>
    public class IdentitySettings
    {
        public string DeveloperName { get; set; } = string.Empty;
        public string AppName { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
    }
}
