using App.Modules.Sys.Substrate.Contracts.Social;

namespace App.Modules.Sys.Infrastructure.Web.Social;

/// <summary>
/// Lite implementation of IPersonIdentityInfo using User claims.
/// Used when Social module is not deployed.
/// </summary>
internal sealed class LitePersonIdentity : IPersonIdentityInfo
{
    /// <inheritdoc />
    public Guid Id { get; init; }

    /// <inheritdoc />
    public Guid PersonId { get; init; }

    /// <inheritdoc />
    public string? DisplayName { get; init; }

    /// <inheritdoc />
    public string? ProfileImageUrl { get; init; }

    /// <inheritdoc />
    public string? Email { get; init; }

    /// <inheritdoc />
    public bool IsPrimary { get; init; } = true;

    /// <inheritdoc />
    public bool IsLiteMode => true;

    /// <summary>
    /// Create a LitePersonIdentity from user claims.
    /// </summary>
    public static LitePersonIdentity FromUserClaims(
        Guid userId,
        string? email,
        string? name,
        string? preferredUsername,
        string? pictureUrl)
    {
        // Priority for display name: name > preferred_username > email prefix
        var displayName = name
            ?? preferredUsername
            ?? (email?.Contains('@') == true ? email.Split('@')[0] : email)
            ?? "User";

        return new LitePersonIdentity
        {
            Id = userId,
            PersonId = userId, // In lite mode, person = user
            DisplayName = displayName,
            ProfileImageUrl = pictureUrl,
            Email = email,
            IsPrimary = true
        };
    }
}
