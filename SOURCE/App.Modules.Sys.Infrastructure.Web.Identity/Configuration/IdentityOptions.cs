namespace App.Modules.Sys.Infrastructure.Web.Identity.Configuration;

/// <summary>
/// Configuration options for identity providers.
/// </summary>
public class IdentityOptions
{
    /// <summary>
    /// Configuration section name.
    /// </summary>
    public const string SectionName = "Identity";

    /// <summary>
    /// Enable Azure AD (Microsoft Entra ID) for enterprise SSO.
    /// FedRAMP High authorized.
    /// </summary>
    public bool UseAzureAd { get; set; }

    /// <summary>
    /// Enable Azure AD B2C for consumer identity.
    /// Supports local accounts + social logins.
    /// FedRAMP High authorized.
    /// </summary>
    public bool UseAzureAdB2C { get; set; }

    /// <summary>
    /// Enable Duende IdentityServer for self-hosted OIDC.
    /// Requires commercial license for production.
    /// Use for air-gapped/on-premises scenarios.
    /// </summary>
    public bool UseDuendeIdentityServer { get; set; }

    /// <summary>
    /// Enable local accounts for offline/fallback scenarios.
    /// Uses PBKDF2 password hashing.
    /// </summary>
    public bool EnableLocalAccounts { get; set; }

    /// <summary>
    /// Default scheme to use when multiple providers are configured.
    /// </summary>
    public string DefaultScheme { get; set; } = "AzureAd";

    /// <summary>
    /// Azure AD configuration.
    /// </summary>
    public AzureAdOptions AzureAd { get; set; } = new();

    /// <summary>
    /// Azure AD B2C configuration.
    /// </summary>
    public AzureAdB2COptions AzureAdB2C { get; set; } = new();

    /// <summary>
    /// Duende IdentityServer configuration (self-hosted).
    /// </summary>
    public DuendeIdentityServerOptions DuendeIdentityServer { get; set; } = new();

    /// <summary>
    /// Local account configuration.
    /// </summary>
    public LocalAccountOptions LocalAccounts { get; set; } = new();
}

/// <summary>
/// Azure AD (Microsoft Entra ID) configuration.
/// </summary>
public class AzureAdOptions
{
    /// <summary>
    /// Azure AD instance URL.
    /// Default: https://login.microsoftonline.com/
    /// For government: https://login.microsoftonline.us/
    /// </summary>
    public string Instance { get; set; } = "https://login.microsoftonline.com/";

    /// <summary>
    /// Azure AD tenant ID.
    /// </summary>
    public string TenantId { get; set; } = string.Empty;

    /// <summary>
    /// Application (client) ID from Azure AD app registration.
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// Client secret for confidential client flows.
    /// </summary>
    public string? ClientSecret { get; set; }

    /// <summary>
    /// Callback path for authentication response.
    /// </summary>
    public string CallbackPath { get; set; } = "/signin-oidc";

    /// <summary>
    /// Signed-out callback path.
    /// </summary>
    public string SignedOutCallbackPath { get; set; } = "/signout-callback-oidc";

    /// <summary>
    /// Scopes to request.
    /// </summary>
    public string[] Scopes { get; set; } = ["openid", "profile", "email"];
}

/// <summary>
/// Azure AD B2C configuration.
/// </summary>
public class AzureAdB2COptions
{
    /// <summary>
    /// B2C instance URL.
    /// Format: https://{tenant}.b2clogin.com
    /// </summary>
    public string Instance { get; set; } = string.Empty;

    /// <summary>
    /// B2C domain.
    /// Format: {tenant}.onmicrosoft.com
    /// </summary>
    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// Azure AD B2C tenant ID.
    /// </summary>
    public string TenantId { get; set; } = string.Empty;

    /// <summary>
    /// Application (client) ID from Azure AD B2C app registration.
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// Client secret for confidential client flows.
    /// </summary>
    public string? ClientSecret { get; set; }

    /// <summary>
    /// Sign-up and sign-in policy ID.
    /// </summary>
    public string SignUpSignInPolicyId { get; set; } = "B2C_1_SignUpSignIn";

    /// <summary>
    /// Password reset policy ID.
    /// </summary>
    public string? ResetPasswordPolicyId { get; set; }

    /// <summary>
    /// Profile edit policy ID.
    /// </summary>
    public string? EditProfilePolicyId { get; set; }

    /// <summary>
    /// Callback path for authentication response.
    /// </summary>
    public string CallbackPath { get; set; } = "/signin-oidc-b2c";

    /// <summary>
    /// Scopes to request.
    /// </summary>
    public string[] Scopes { get; set; } = ["openid", "profile", "email", "offline_access"];
}

/// <summary>
/// Local account configuration.
/// </summary>
public class LocalAccountOptions
{
    /// <summary>
    /// Minimum password length.
    /// </summary>
    public int MinimumPasswordLength { get; set; } = 12;

    /// <summary>
    /// Require uppercase letter in password.
    /// </summary>
    public bool RequireUppercase { get; set; } = true;

    /// <summary>
    /// Require lowercase letter in password.
    /// </summary>
    public bool RequireLowercase { get; set; } = true;

    /// <summary>
    /// Require digit in password.
    /// </summary>
    public bool RequireDigit { get; set; } = true;

    /// <summary>
    /// Require special character in password.
    /// </summary>
    public bool RequireSpecialCharacter { get; set; } = true;

    /// <summary>
    /// Maximum failed login attempts before lockout.
    /// </summary>
    public int MaxFailedAccessAttempts { get; set; } = 5;

    /// <summary>
    /// Lockout duration in minutes.
    /// </summary>
    public int LockoutDurationMinutes { get; set; } = 15;

    /// <summary>
    /// Session timeout in minutes.
    /// </summary>
    public int SessionTimeoutMinutes { get; set; } = 480; // 8 hours

    /// <summary>
    /// Require email confirmation before login.
    /// </summary>
    public bool RequireEmailConfirmation { get; set; } = true;
}
