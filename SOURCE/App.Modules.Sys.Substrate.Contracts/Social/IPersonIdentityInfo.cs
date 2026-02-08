namespace App.Modules.Sys.Substrate.Contracts.Social;

/// <summary>
/// Minimal person identity information that Sys needs.
/// 
/// This contract is defined in Sys so that:
/// 1. Sys can function without the Social module being deployed
/// 2. Social module provides the full implementation when available
/// 3. Sys.Infrastructure provides a lite fallback using User claims
/// 
/// DESIGN PRINCIPLE:
/// Person/Identity is so fundamental that virtually every app needs it.
/// However, for very lite apps that only need Users (no social graph),
/// a lite implementation using IdP claims (email, name, picture) suffices.
/// </summary>
public interface IPersonIdentityInfo
{
    /// <summary>
    /// Unique identifier for this identity.
    /// In lite mode, this is the User ID.
    /// In full mode, this is the PersonIdentity ID.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// The person this identity belongs to.
    /// In lite mode, this equals the User ID.
    /// In full mode, this is the Person ID from Social module.
    /// </summary>
    Guid PersonId { get; }

    /// <summary>
    /// Display name for UI.
    /// In lite mode: from IdP claims (name, preferred_username, email).
    /// In full mode: from PersonIdentity.DisplayName.
    /// </summary>
    string? DisplayName { get; }

    /// <summary>
    /// URL to profile image.
    /// In lite mode: from IdP claims (picture).
    /// In full mode: from PersonIdentity.ProfileImageUrl.
    /// </summary>
    string? ProfileImageUrl { get; }

    /// <summary>
    /// Email address (for notifications, display).
    /// In lite mode: from User.Email or IdP claims.
    /// In full mode: from PersonIdentity or primary ContactChannel.
    /// </summary>
    string? Email { get; }

    /// <summary>
    /// Whether this is the primary/default identity.
    /// In lite mode: always true (only one identity per user).
    /// In full mode: true if this is the primary PersonIdentity.
    /// </summary>
    bool IsPrimary { get; }

    /// <summary>
    /// Whether this identity is from the lite fallback (User claims)
    /// or the full Social module (PersonIdentity).
    /// </summary>
    bool IsLiteMode { get; }
}
