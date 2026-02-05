namespace App.Modules.Sys.Infrastructure.Domains.Identity.Constants;

/// <summary>
/// Standard claim type identifiers for JWT tokens and identity claims.
/// Provides constants for Microsoft identity platform and custom claims.
/// </summary>
/// <remarks>
/// These claim types follow standard schemas:
/// - Microsoft identity platform (schemas.microsoft.com)
/// - WS-Federation (schemas.microsoft.com/ws/2008/06)
/// - Custom application claims (schemas.org.tld)
/// 
/// Use these constants instead of hardcoded strings when accessing claims from ClaimsPrincipal.
/// </remarks>
public static class ClaimTitles
{
    /// <summary>
    /// Scope claim identifier used in Microsoft identity platform.
    /// Contains the permissions/scopes granted to the token.
    /// </summary>
    /// <remarks>
    /// Standard URI: "http://schemas.microsoft.com/identity/claims/scope"
    /// 
    /// Example usage:
    /// <code>
    /// var scopes = principal.FindFirst(ClaimTitles.ScopeElementId)?.Value;
    /// </code>
    /// 
    /// Typical values: "user.read", "openid profile", etc.
    /// </remarks>
    public const string ScopeElementId = "http://schemas.microsoft.com/identity/claims/scope";

    /// <summary>
    /// Role claim identifier from WS-Federation standard.
    /// Contains user role memberships (e.g., "Administrator", "User").
    /// </summary>
    /// <remarks>
    /// Standard URI: "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    /// 
    /// Example usage:
    /// <code>
    /// var roles = principal.FindAll(ClaimTitles.RoleElementId).Select(c => c.Value);
    /// </code>
    /// 
    /// Used by [Authorize(Roles = "Admin")] attribute matching.
    /// </remarks>
    public const string RoleElementId = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

    /// <summary>
    /// Culture/locale claim identifier for user's preferred language.
    /// Custom claim for multilingual application support.
    /// </summary>
    /// <remarks>
    /// Custom URI: "http://schemas.org.tld/identity/claims/culture"
    /// 
    /// Example values: "en-US", "fr-FR", "ja-JP"
    /// 
    /// Used to determine UI language, date/time formatting, and localization.
    /// </remarks>
    public const string CultureElementId = "http://schemas.org.tld/identity/claims/culture";

    /// <summary>
    /// Tenant/workspace identifier claim for multi-tenant applications.
    /// Custom claim indicating which tenant/workspace the user belongs to.
    /// </summary>
    /// <remarks>
    /// Custom URI: "http://schemas.org.tld/identity/claims/tenant"
    /// 
    /// Used for:
    /// - Multi-tenant data isolation
    /// - Workspace-scoped permissions
    /// - Tenant-specific configuration
    /// </remarks>
    public const string PrincipalKeyElementId = "http://schemas.org.tld/identity/claims/tenant";

    /// <summary>
    /// Object identifier claim from Microsoft identity platform.
    /// Unique immutable identifier for the user in Azure AD/Entra ID.
    /// </summary>
    /// <remarks>
    /// Standard URI: "http://schemas.microsoft.com/identity/claims/objectidentifier"
    /// 
    /// This is the primary key for the user in Azure AD.
    /// Use this (not email or username) as the stable user identifier.
    /// 
    /// Format: GUID (e.g., "a1b2c3d4-e5f6-7890-abcd-ef1234567890")
    /// </remarks>
    public const string ObjectIdElementId = "http://schemas.microsoft.com/identity/claims/objectidentifier";

    /// <summary>
    /// User identifier claim (custom short name).
    /// Simple claim name for accessing user ID in application code.
    /// </summary>
    /// <remarks>
    /// Short name: "UserId"
    /// 
    /// This is an application-specific claim, typically mapped from
    /// ObjectIdElementId or NameIdentifier during token validation.
    /// </remarks>
    public const string UserIdentifier = "UserId";

    /// <summary>
    /// Session identifier claim.
    /// Identifies the user's current session for tracking and audit purposes.
    /// </summary>
    /// <remarks>
    /// Short name: "SessionId"
    /// 
    /// Used for:
    /// - Session management and tracking
    /// - Audit logging (correlate actions to session)
    /// - Concurrent session control
    /// </remarks>
    public const string SessionIdentifier = "SessionId";

    /// <summary>
    /// Unique session identifier claim.
    /// Globally unique identifier for the session across all tenants/workspaces.
    /// </summary>
    /// <remarks>
    /// Short name: "UniqueSessionId"
    /// 
    /// Differs from SessionId in that it's guaranteed unique across:
    /// - All users
    /// - All tenants/workspaces
    /// - All time periods
    /// 
    /// Format: Typically a GUID or cryptographically secure random identifier.
    /// </remarks>
    public const string UniqueSessionIdentifier = "UniqueSessionId";
}

