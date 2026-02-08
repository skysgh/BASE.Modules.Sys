namespace App.Modules.Sys.Infrastructure.Web.Identity.Configuration;

/// <summary>
/// Configuration options for Duende IdentityServer.
/// Used for self-hosted OIDC scenarios.
/// </summary>
public class DuendeIdentityServerOptions
{
    /// <summary>
    /// Configuration section name.
    /// </summary>
    public const string SectionName = "DuendeIdentityServer";

    /// <summary>
    /// Whether Duende IdentityServer is enabled.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// The issuer URI for the identity server.
    /// Should match the URL where the server is hosted.
    /// </summary>
    public string IssuerUri { get; set; } = string.Empty;

    /// <summary>
    /// Whether to require HTTPS for metadata endpoints.
    /// Should be true in production.
    /// </summary>
    public bool RequireHttpsMetadata { get; set; } = true;

    /// <summary>
    /// Enable automatic cleanup of expired tokens.
    /// </summary>
    public bool EnableTokenCleanup { get; set; } = true;

    /// <summary>
    /// Interval in seconds between token cleanup runs.
    /// </summary>
    public int TokenCleanupInterval { get; set; } = 3600;

    /// <summary>
    /// Access token lifetime in seconds.
    /// </summary>
    public int AccessTokenLifetimeSeconds { get; set; } = 3600;

    /// <summary>
    /// Identity token lifetime in seconds.
    /// </summary>
    public int IdentityTokenLifetimeSeconds { get; set; } = 300;

    /// <summary>
    /// Refresh token lifetime in seconds.
    /// </summary>
    public int RefreshTokenLifetimeSeconds { get; set; } = 2592000; // 30 days

    /// <summary>
    /// Whether to allow offline access (refresh tokens).
    /// </summary>
    public bool AllowOfflineAccess { get; set; } = true;

    /// <summary>
    /// Whether to update access token claims on refresh.
    /// </summary>
    public bool UpdateAccessTokenClaimsOnRefresh { get; set; } = true;

    /// <summary>
    /// Refresh token usage mode.
    /// OneTimeOnly: Token can only be used once, new token issued on refresh.
    /// ReUse: Token can be reused until expiry.
    /// </summary>
    public string RefreshTokenUsage { get; set; } = "OneTimeOnly";

    /// <summary>
    /// Refresh token expiration mode.
    /// Sliding: Refresh token lifetime refreshes on use.
    /// Absolute: Refresh token expires at fixed time.
    /// </summary>
    public string RefreshTokenExpiration { get; set; } = "Sliding";

    /// <summary>
    /// Signing credential configuration.
    /// </summary>
    public SigningCredentialOptions SigningCredential { get; set; } = new();
}

/// <summary>
/// Signing credential options for Duende IdentityServer.
/// </summary>
public class SigningCredentialOptions
{
    /// <summary>
    /// Type of signing credential.
    /// </summary>
    public SigningCredentialType Type { get; set; } = SigningCredentialType.Development;

    /// <summary>
    /// Path to certificate file (for File type).
    /// </summary>
    public string? CertificatePath { get; set; }

    /// <summary>
    /// Certificate password (for File type).
    /// </summary>
    public string? CertificatePassword { get; set; }

    /// <summary>
    /// Certificate thumbprint (for Store type).
    /// </summary>
    public string? CertificateThumbprint { get; set; }

    /// <summary>
    /// Azure Key Vault URL (for KeyVault type).
    /// </summary>
    public string? KeyVaultUrl { get; set; }

    /// <summary>
    /// Azure Key Vault certificate name (for KeyVault type).
    /// </summary>
    public string? KeyVaultCertificateName { get; set; }
}

/// <summary>
/// Type of signing credential for token signing.
/// </summary>
public enum SigningCredentialType
{
    /// <summary>
    /// Development/ephemeral key (not for production).
    /// </summary>
    Development,

    /// <summary>
    /// Certificate from file.
    /// </summary>
    File,

    /// <summary>
    /// Certificate from Windows certificate store.
    /// </summary>
    Store,

    /// <summary>
    /// Certificate from Azure Key Vault.
    /// </summary>
    KeyVault
}
