namespace App.Modules.Sys.Domain.Identity;

/// <summary>
/// Stores a hashed credential for a LocalUser.
/// 
/// SECURITY DESIGN:
/// - Each credential has its own unique salt
/// - Password is NEVER stored - only salted hash
/// - Uses PBKDF2 with high iteration count (via ASP.NET Core Identity)
/// - Supports credential rotation (multiple valid credentials during transition)
/// 
/// WHY SEPARATE ENTITY:
/// 1. Each credential has its own salt (not shared)
/// 2. Supports multiple credential types (password, recovery codes)
/// 3. Supports credential history (prevent reuse)
/// 4. Can expire credentials independently
/// 
/// HASH FORMAT:
/// Uses ASP.NET Core Identity PasswordHasher format:
/// - Version 3 (V3): PBKDF2 with HMAC-SHA256, 128-bit salt, 256-bit subkey, 10000 iterations
/// - Format: { 0x01, salt[16], subkey[32] } = 49 bytes, Base64 encoded
/// </summary>
public class LocalUserCredential
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user this credential belongs to.
    /// </summary>
    public Guid LocalUserId { get; set; }

    /// <summary>
    /// Type of credential.
    /// </summary>
    public CredentialType Type { get; set; } = CredentialType.Password;

    /// <summary>
    /// The hashed credential value.
    /// For passwords: PBKDF2 hash with embedded salt (ASP.NET Core Identity format).
    /// NEVER contains plain text.
    /// </summary>
    /// <remarks>
    /// The salt is embedded in the hash for PBKDF2.
    /// Format: Base64({ version_byte, salt[16], subkey[32] })
    /// Each credential has a unique salt generated at hash time.
    /// </remarks>
    public string HashedValue { get; set; } = string.Empty;

    /// <summary>
    /// When this credential was created.
    /// </summary>
    public DateTime CreatedOnDateTimeUtc { get; set; }

    /// <summary>
    /// When this credential expires (if applicable).
    /// Null means no expiration.
    /// </summary>
    public DateTime? ExpiresOnDateTimeUtc { get; set; }

    /// <summary>
    /// Whether this credential is active.
    /// Inactive credentials cannot be used for authentication.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Whether this credential was used for first-time setup
    /// and must be changed on first login.
    /// </summary>
    public bool RequireChangeOnNextLogin { get; set; }

    /// <summary>
    /// When this credential was last used for authentication.
    /// </summary>
    public DateTime? LastUsedDateTimeUtc { get; set; }

    /// <summary>
    /// Description/label for this credential (for UI).
    /// e.g., "Primary password", "Recovery code set 1"
    /// </summary>
    public string? Description { get; set; }

    // Navigation

    /// <summary>
    /// The user this credential belongs to.
    /// </summary>
    public virtual LocalUser? LocalUser { get; set; }
}

/// <summary>
/// Types of credentials that can be stored.
/// </summary>
public enum CredentialType
{
    /// <summary>
    /// Standard password (hashed with PBKDF2).
    /// </summary>
    Password = 1,

    /// <summary>
    /// Temporary password (must be changed on first use).
    /// </summary>
    TemporaryPassword = 2,

    /// <summary>
    /// Recovery code for account recovery.
    /// </summary>
    RecoveryCode = 3,

    /// <summary>
    /// TOTP authenticator secret (for 2FA).
    /// </summary>
    TotpSecret = 4,

    /// <summary>
    /// API key for service accounts.
    /// </summary>
    ApiKey = 5
}
