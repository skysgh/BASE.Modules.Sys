using System;

namespace App.Modules.Sys.Domain.Identity
{
    /// <summary>
    /// Links User to external authentication provider.
    /// Supports multi-provider authentication (user can login via Google, Microsoft, Local, etc.)
    /// </summary>
    public class UserIdentity
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Which user this identity belongs to
        /// </summary>
        public Guid UserId { get; set; }
        
        /// <summary>
        /// Navigation property to User
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Authentication provider.
        /// Examples: "Local", "Google", "Microsoft", "AzureAD", "GitHub"
        /// </summary>
        public string Provider { get; set; } = string.Empty;

        /// <summary>
        /// User ID from the external provider.
        /// For "Google" this might be the Google User ID.
        /// For "Local" this is the username or email.
        /// </summary>
        public string ProviderUserId { get; set; } = string.Empty;

        /// <summary>
        /// Email associated with this provider (if applicable).
        /// For Local provider: the email used for login
        /// For OAuth providers: email from OAuth profile
        /// Different providers may have different emails for same User.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Password hash (ONLY for Provider="Local").
        /// Null for external providers.
        /// Use Argon2 or bcrypt for hashing.
        /// </summary>
        public string? PasswordHash { get; set; }

        /// <summary>
        /// When this identity was linked
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Last time this provider was used for authentication
        /// </summary>
        public DateTime? LastUsedAt { get; set; }
    }
}
