namespace App.Modules.Sys.Domain.Identity;

/// <summary>
/// Stores refresh tokens for OIDC token refresh flow.
/// 
/// SECURITY DESIGN:
/// - Refresh tokens are hashed (like passwords)
/// - Each token has a unique ID for revocation
/// - Tokens can be individually revoked
/// - Family tracking for refresh token rotation
/// </summary>
public class LocalUserRefreshToken
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The user this token belongs to.
    /// </summary>
    public Guid LocalUserId { get; set; }

    /// <summary>
    /// Handle/identifier for the token (used for lookup).
    /// This is NOT the actual token - it's a lookup key.
    /// </summary>
    public string TokenHandle { get; set; } = string.Empty;

    /// <summary>
    /// Hashed value of the actual refresh token.
    /// The actual token is only sent to the client and never stored.
    /// </summary>
    public string HashedToken { get; set; } = string.Empty;

    /// <summary>
    /// When this token was issued.
    /// </summary>
    public DateTime IssuedDateTimeUtc { get; set; }

    /// <summary>
    /// When this token expires.
    /// </summary>
    public DateTime ExpiresDateTimeUtc { get; set; }

    /// <summary>
    /// When this token was consumed (used to get a new access token).
    /// Null if not yet used.
    /// </summary>
    public DateTime? ConsumedDateTimeUtc { get; set; }

    /// <summary>
    /// Whether this token has been revoked.
    /// </summary>
    public bool IsRevoked { get; set; }

    /// <summary>
    /// Reason for revocation (if revoked).
    /// </summary>
    public string? RevocationReason { get; set; }

    /// <summary>
    /// Token family ID for rotation tracking.
    /// All tokens in a rotation chain share the same family.
    /// If a token from a family is reused after rotation, revoke entire family.
    /// </summary>
    public Guid FamilyId { get; set; }

    /// <summary>
    /// The token this one replaced (for rotation tracking).
    /// </summary>
    public Guid? ReplacedByTokenId { get; set; }

    /// <summary>
    /// Client ID that this token was issued to.
    /// </summary>
    public string? ClientId { get; set; }

    /// <summary>
    /// Device/session identifier for multi-device management.
    /// </summary>
    public string? DeviceId { get; set; }

    /// <summary>
    /// IP address from which token was issued.
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// User agent from which token was issued.
    /// </summary>
    public string? UserAgent { get; set; }

    // Navigation

    /// <summary>
    /// The user this token belongs to.
    /// </summary>
    public virtual LocalUser? LocalUser { get; set; }
}
