using App.Modules.Sys.Domain.Authorization;
using App.Modules.Sys.Shared;
using System;
using System.Collections.Generic;

namespace App.Modules.Sys.Domain.Identity
{
    /// <summary>
    /// Runtime user identity - authentication and authorization ONLY.
    /// THIN by design - no display names, no profile data, no email.
    /// Use IdentityLink to connect to Person (social domain) when needed.
    /// </summary>
    /// <remarks>
    /// Design principles:
    /// - NO EMAIL: Email is provider-specific, stored in UserIdentity
    /// - NO FLAGS: Use SystemPermissions (e.g., "System.Admin")
    /// - User = Can login (authentication)
    /// - Person = Human being (social domain, separate)
    /// - Link via IdentityLink (opaque bridge)
    /// </remarks>
    public class User : IUser
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }

        /// <inheritdoc/>
        public bool IsActive { get; set; } = true;

        /// <inheritdoc/>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <inheritdoc/>
        public DateTime? LastLoginAt { get; set; }

        // Navigation properties
        
        /// <summary>
        /// Authentication providers linked to this user.
        /// User can login via multiple providers (Google, Microsoft, Local, etc.)
        /// Each provider may have different email/username.
        /// </summary>
        public List<UserIdentity> Identities { get; set; } = new();

        /// <summary>
        /// System-level permissions assigned directly to this user.
        /// For runtime authorization (e.g., "System.Configure", "System.Admin")
        /// </summary>
        public List<UserSystemPermission> SystemPermissions { get; set; } = new();

        /// <summary>
        /// Link to Person entity (social domain) if exists.
        /// Opaque to Sys - we don't interpret Person structure.
        /// </summary>
        public IdentityLink? IdentityLink { get; set; }
    }
}

