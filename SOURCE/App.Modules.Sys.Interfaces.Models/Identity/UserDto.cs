using System;

namespace App.Modules.Sys.Interfaces.Models.Identity
{
    /// <summary>
    /// Data Transfer Object for User.
    /// API contract - exposed via REST/gRPC/SignalR.
    /// </summary>
    /// <remarks>
    /// Design notes:
    /// - Serialization-friendly (all public properties)
    /// - DateTime as ISO 8601 string for API consistency
    /// - No sensitive data (passwords, etc.)
    /// - Security filtering applied at controller level
    /// </remarks>
    public class UserDto
    {
        /// <summary>
        /// Unique user identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Whether user can authenticate
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// System administrator flag
        /// </summary>
        public bool IsSystemAdmin { get; set; }

        /// <summary>
        /// When user was created (ISO 8601)
        /// </summary>
        public string CreatedAt { get; set; } = string.Empty;

        /// <summary>
        /// Last login timestamp (ISO 8601, nullable)
        /// </summary>
        public string? LastLoginAt { get; set; }
    }
}
