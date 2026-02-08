namespace App.Modules.Sys.Domain.Identity;

/// <summary>
/// A local user account that can authenticate directly with this system.
/// 
/// SECURITY DESIGN:
/// This entity stores ONLY the minimum needed for authentication.
/// Password is NEVER stored, only a salted hash.
/// Each credential has its own unique salt.
/// Email is stored for login lookup. Consider encryption at rest.
/// 
/// RELATIONSHIP TO PERSON:
/// LocalUser is an authentication credential.
/// Person is a social entity in the Social module.
/// They are linked via PersonToUserAssignment.
/// A Person may have zero, one, or many LocalUser accounts.
/// 
/// WHEN TO USE:
/// Super admin - cannot use external IdP before system exists.
/// Offline scenarios.
/// B2B service accounts.
/// Development and testing.
/// 
/// PREFER EXTERNAL OIDC:
/// For regular users, prefer external IdP such as Azure AD or Google.
/// External IdPs handle password policy, MFA, and breach detection.
/// Less liability for us.
/// </summary>
public class LocalUser
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Email address for login.
    /// This is the primary identifier for local authentication.
    /// Unique across the system.
    /// </summary>
    /// <remarks>
    /// Consider encrypting at rest to protect against DB breaches.
    /// However, we need to be able to query by email for login.
    /// Options: deterministic encryption or separate lookup hash.
    /// </remarks>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Normalized email for case-insensitive lookups.
    /// Uppercase, trimmed.
    /// </summary>
    public string NormalizedEmail { get; set; } = string.Empty;

    /// <summary>
    /// Whether email has been confirmed.
    /// </summary>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Whether this local account is enabled.
    /// Disabled accounts cannot authenticate.
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Whether this account is locked out.
    /// </summary>
    public bool IsLockedOut { get; set; }

    /// <summary>
    /// When the lockout ends (UTC).
    /// Null means locked indefinitely (admin must unlock).
    /// </summary>
    public DateTime? LockoutEndDateTimeUtc { get; set; }

    /// <summary>
    /// Failed login attempt count (for lockout policy).
    /// </summary>
    public int AccessFailedCount { get; set; }

    /// <summary>
    /// Security stamp - changes when security-critical data changes.
    /// Used to invalidate existing tokens/sessions.
    /// </summary>
    public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Concurrency stamp for optimistic concurrency.
    /// </summary>
    public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// When the account was created.
    /// </summary>
    public DateTime CreatedOnDateTimeUtc { get; set; }

    /// <summary>
    /// When the account was last modified.
    /// </summary>
    public DateTime? LastModifiedOnDateTimeUtc { get; set; }

    /// <summary>
    /// When the user last successfully authenticated.
    /// </summary>
    public DateTime? LastLoginDateTimeUtc { get; set; }

    /// <summary>
    /// Whether MFA is enabled for this account.
    /// </summary>
    public bool TwoFactorEnabled { get; set; }

    /// <summary>
    /// Phone number for MFA (if configured).
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Whether phone number is confirmed.
    /// </summary>
    public bool PhoneNumberConfirmed { get; set; }

    // Navigation properties

    /// <summary>
    /// Credentials for this user (password, etc.).
    /// Separate entity so each credential has its own salt.
    /// </summary>
    public virtual ICollection<LocalUserCredential> Credentials { get; set; } = new List<LocalUserCredential>();

    /// <summary>
    /// External login providers linked to this account.
    /// </summary>
    public virtual ICollection<LocalUserExternalLogin> ExternalLogins { get; set; } = new List<LocalUserExternalLogin>();

    /// <summary>
    /// Refresh tokens for this user.
    /// </summary>
    public virtual ICollection<LocalUserRefreshToken> RefreshTokens { get; set; } = new List<LocalUserRefreshToken>();
}
