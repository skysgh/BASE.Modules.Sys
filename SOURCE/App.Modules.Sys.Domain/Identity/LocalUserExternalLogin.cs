namespace App.Modules.Sys.Domain.Identity;

/// <summary>
/// Links a LocalUser to an external identity provider login.
/// 
/// This allows users to authenticate via external OIDC providers
/// (Azure AD, Google, Facebook, etc.) while having a local account
/// for storing preferences and linking to Person.
/// </summary>
public class LocalUserExternalLogin
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The local user this external login is linked to.
    /// </summary>
    public Guid LocalUserId { get; set; }

    /// <summary>
    /// The login provider (e.g., "Google", "AzureAD", "Facebook").
    /// </summary>
    public string LoginProvider { get; set; } = string.Empty;

    /// <summary>
    /// The unique identifier for this user at the external provider.
    /// This is the "sub" claim from OIDC.
    /// </summary>
    public string ProviderKey { get; set; } = string.Empty;

    /// <summary>
    /// Display name from the provider (for UI reference).
    /// </summary>
    public string? ProviderDisplayName { get; set; }

    /// <summary>
    /// When this link was created.
    /// </summary>
    public DateTime CreatedOnDateTimeUtc { get; set; }

    /// <summary>
    /// When this link was last used for authentication.
    /// </summary>
    public DateTime? LastUsedDateTimeUtc { get; set; }

    // Navigation

    /// <summary>
    /// The local user this login is linked to.
    /// </summary>
    public virtual LocalUser? LocalUser { get; set; }
}
