using System;

namespace App.Modules.Sys.Shared
{
    /// <summary>
    /// Contract for runtime user identity (authentication/authorization only).
    /// This is NOT social identity - it's purely operational.
    /// User is thin by design - no names, no profile data, no email.
    /// Link to Person (social domain) via IdentityLink when needed.
    /// </summary>
    /// <remarks>
    /// Design notes:
    /// - NO EMAIL: Email is provider-specific, stored in UserIdentity
    /// - NO FLAGS: Use SystemPermissions instead (e.g., "System.Admin")
    /// - Focused on runtime authentication/authorization only
    /// </remarks>
    public interface IUser
    {
        /// <summary>
        /// Unique user identifier
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Whether user can authenticate
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// When user was created
        /// </summary>
        DateTime CreatedAt { get; }

        /// <summary>
        /// Last successful login (across all providers)
        /// </summary>
        DateTime? LastLoginAt { get; }
    }
}

