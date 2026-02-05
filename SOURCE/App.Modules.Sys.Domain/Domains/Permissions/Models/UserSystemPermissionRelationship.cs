using App.Modules.Sys.Domain.Domains.Identity;
using System;
using System.Diagnostics.CodeAnalysis;

namespace App.Modules.Sys.Domain.Domains.Permissions.Models
{
    /// <summary>
    /// Links User to SystemPermission (direct assignment).
    /// Join table for M:N relationship.
    /// </summary>
    [SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix",
        Justification = "UserSystemPermission is the correct domain term")]
    public class UserSystemPermissionRelationship
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User who has the permission
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Navigation property to User
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Permission key being granted
        /// </summary>
        public string PermissionKey { get; set; } = string.Empty;

        /// <summary>
        /// Navigation property to Permission
        /// </summary>
        public SystemPermission Permission { get; set; } = null!;

        /// <summary>
        /// When permission was granted
        /// </summary>
        public DateTime GrantedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Who granted the permission (for audit)
        /// </summary>
        public string? GrantedBy { get; set; }
    }
}
