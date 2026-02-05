namespace App.Modules.Sys.Infrastructure.Domains.Identity.Constants;

/// <summary>
/// Standard JWT token claim identifiers.
/// Provides constants for identity provider claims and token metadata.
/// </summary>
/// <remarks>
/// These constants follow JWT RFC standards and Microsoft identity platform conventions:
/// - Identity provider claims (schemas.microsoft.com)
/// - Subject/name identifier (schemas.xmlsoap.org)
/// - Standard JWT registered claims (RFC 7519)
/// 
/// Use these constants when parsing JWT tokens or validating token claims.
/// </remarks>
public static class TokenTitles
{
    /// <summary>
    /// Identity provider claim identifier.
    /// Specifies which identity provider authenticated the user.
    /// </summary>
    /// <remarks>
    /// Standard URI: "http://schemas.microsoft.com/identity/claims/identityprovider"
    /// 
    /// Example values:
    /// - "live.com" → Microsoft Account
    /// - "facebook.com" → Facebook login
    /// - "google.com" → Google login
    /// - "{tenant}.onmicrosoft.com" → Azure AD tenant
    /// 
    /// Used for:
    /// - Federated identity tracking
    /// - Audit logging (know which IdP was used)
    /// - Provider-specific logic
    /// </remarks>
    public const string IdpIdentifierId = "http://schemas.microsoft.com/identity/claims/identityprovider";

    /// <summary>
    /// Subject identifier claim (unique user ID from issuing authority).
    /// Standard SAML/WS-Federation claim for user's unique identifier.
    /// </summary>
    /// <remarks>
    /// Standard URI: "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
    /// 
    /// This is the "sub" (subject) claim in JWT format.
    /// 
    /// Represents:
    /// - Unique identifier for the user in the issuing system
    /// - Stable across sessions (doesn't change on password reset, etc.)
    /// - Primary key for user in identity provider
    /// 
    /// Use this as the authoritative user ID when federating with external IdPs.
    /// </remarks>
    public const string SubjectIdentifierId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

    /// <summary>
    /// Token expiration time claim (exp).
    /// Standard JWT claim indicating when the token expires.
    /// </summary>
    /// <remarks>
    /// Short name: "exp" (per RFC 7519)
    /// 
    /// Format: NumericDate (seconds since Unix epoch)
    /// Example: 1609459200 = 2021-01-01 00:00:00 UTC
    /// 
    /// Usage:
    /// <code>
    /// var exp = long.Parse(token.Claims.First(c => c.Type == TokenTitles.ExpiryId).Value);
    /// var expiryTime = DateTimeOffset.FromUnixTimeSeconds(exp);
    /// if (expiryTime &lt; DateTimeOffset.UtcNow)
    /// {
    ///     // Token expired
    /// }
    /// </code>
    /// 
    /// Note: Token validation middleware automatically checks expiry.
    /// </remarks>
    public const string ExpiryId = "exp";

    /// <summary>
    /// Token issued-at time claim (iat).
    /// Standard JWT claim indicating when the token was created.
    /// </summary>
    /// <remarks>
    /// Short name: "iat" (per RFC 7519)
    /// 
    /// Format: NumericDate (seconds since Unix epoch)
    /// Example: 1609459200 = 2021-01-01 00:00:00 UTC
    /// 
    /// Used for:
    /// - Token age validation (reject if too old)
    /// - Replay attack prevention
    /// - Audit logging (when was token issued)
    /// - Clock skew tolerance calculations
    /// 
    /// Note: Some scenarios reject tokens issued before a certain time
    /// (e.g., after password change, after revocation event).
    /// </remarks>
    public const string IssuedAtId = "iat";
}

