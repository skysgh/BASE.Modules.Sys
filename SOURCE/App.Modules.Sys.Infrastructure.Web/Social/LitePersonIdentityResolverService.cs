using System.Security.Claims;
using App.Modules.Sys.Infrastructure.Services;
using App.Modules.Sys.Shared.Services;
using App.Modules.Sys.Substrate.Contracts.Social;

namespace App.Modules.Sys.Infrastructure.Web.Social;

/// <summary>
/// Lite implementation of IPersonIdentityResolverService using User claims from IdP.
/// Used when Social module is not deployed.
/// 
/// This provides basic display name, email, and profile image from:
/// - OpenID Connect standard claims (name, email, picture)
/// - Azure AD claims (preferred_username, upn)
/// - Generic claims (given_name + family_name)
/// </summary>
public class LitePersonIdentityResolverService : IPersonIdentityResolverService, IHasScopedService
{
    private readonly IRequestContextService _requestContextService;

    /// <summary>
    /// Constructor.
    /// </summary>
    public LitePersonIdentityResolverService(IRequestContextService requestContextService)
    {
        this._requestContextService = requestContextService;
    }

    /// <inheritdoc />
    public bool IsLiteMode => true;

    /// <inheritdoc />
    public Task<IPersonIdentityInfo?> GetCurrentUserIdentityAsync(CancellationToken ct = default)
    {
        var user = this._requestContextService.User;
        if (!this._requestContextService.IsAuthenticated || user == null)
        {
            return Task.FromResult<IPersonIdentityInfo?>(null);
        }

        var identity = this.BuildIdentityFromClaims(user);
        return Task.FromResult<IPersonIdentityInfo?>(identity);
    }

    /// <inheritdoc />
    public Task<IPersonIdentityInfo?> GetIdentityForUserAsync(Guid userId, CancellationToken ct = default)
    {
        // In lite mode, we can only resolve the current user
        // For other users, we'd need to look them up in a user store
        var currentUser = this._requestContextService.User;
        if (!this._requestContextService.IsAuthenticated || currentUser == null)
        {
            return Task.FromResult<IPersonIdentityInfo?>(null);
        }

        var currentUserId = this.GetUserId(currentUser);
        if (currentUserId == userId)
        {
            var identity = this.BuildIdentityFromClaims(currentUser);
            return Task.FromResult<IPersonIdentityInfo?>(identity);
        }

        // For other users, return a minimal identity
        // In a real implementation, you'd query the user store
        return Task.FromResult<IPersonIdentityInfo?>(new LitePersonIdentity
        {
            Id = userId,
            PersonId = userId,
            DisplayName = null, // Unknown
            Email = null,
            ProfileImageUrl = null,
            IsPrimary = true
        });
    }

    /// <inheritdoc />
    public Task<IPersonIdentityInfo?> GetIdentityForUserInWorkspaceAsync(
        Guid userId, 
        Guid workspaceId, 
        CancellationToken ct = default)
    {
        // In lite mode, workspace doesn't affect identity
        _ = workspaceId; // Unused in lite mode
        return this.GetIdentityForUserAsync(userId, ct);
    }

    private LitePersonIdentity BuildIdentityFromClaims(ClaimsPrincipal user)
    {
        var userId = this.GetUserId(user);
        var email = this.GetClaimValue(user, ClaimTypes.Email, "email");
        var name = this.GetDisplayName(user);
        var pictureUrl = this.GetClaimValue(user, "picture");

        return LitePersonIdentity.FromUserClaims(
            userId,
            email,
            name,
            this.GetClaimValue(user, "preferred_username"),
            pictureUrl);
    }

    private Guid GetUserId(ClaimsPrincipal user)
    {
        // Try various claim types for user ID
        var idClaim = this.GetClaimValue(user, ClaimTypes.NameIdentifier, "sub", "oid");
        if (Guid.TryParse(idClaim, out var guid))
        {
            return guid;
        }

        // If not a GUID, generate a deterministic one from the string
        if (!string.IsNullOrEmpty(idClaim))
        {
            return GenerateDeterministicGuid(idClaim);
        }

        return Guid.Empty;
    }

    private string? GetDisplayName(ClaimsPrincipal user)
    {
        // Try full name first
        var name = this.GetClaimValue(user, ClaimTypes.Name, "name");
        if (!string.IsNullOrEmpty(name))
        {
            return name;
        }

        // Try given + family name
        var givenName = this.GetClaimValue(user, ClaimTypes.GivenName, "given_name");
        var familyName = this.GetClaimValue(user, ClaimTypes.Surname, "family_name");
        if (!string.IsNullOrEmpty(givenName) || !string.IsNullOrEmpty(familyName))
        {
            return $"{givenName} {familyName}".Trim();
        }

        // Try preferred username (common in Azure AD)
        return this.GetClaimValue(user, "preferred_username", "upn");
    }

    private string? GetClaimValue(ClaimsPrincipal user, params string[] claimTypes)
    {
        foreach (var claimType in claimTypes)
        {
            var value = user.FindFirst(claimType)?.Value;
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
        }
        return null;
    }

    private static Guid GenerateDeterministicGuid(string input)
    {
        using var md5 = System.Security.Cryptography.MD5.Create();
        var hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
        return new Guid(hash);
    }
}
