using System;

namespace App.Modules.Sys.Application.Models.Identity
{
    /// <summary>
    /// View model for User entity.
    /// Read-optimized, no EF tracking, no navigation properties.
    /// Used by Application services to return data.
    /// </summary>
    public class UserViewModel
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
        /// When user was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Last login timestamp
        /// </summary>
        public DateTime? LastLoginAt { get; set; }
    }
}

