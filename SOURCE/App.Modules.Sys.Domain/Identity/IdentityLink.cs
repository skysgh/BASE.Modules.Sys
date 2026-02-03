using System;

namespace App.Modules.Sys.Domain.Identity
{
    /// <summary>
    /// Opaque link between Sys User and Social Person.
    /// This is a pure integration fact - Sys doesn't interpret Person structure.
    /// Allows Social module to be developed independently later.
    /// </summary>
    public class IdentityLink
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User in Sys domain (runtime identity)
        /// </summary>
        public Guid UserId { get; set; }
        
        /// <summary>
        /// Navigation property to User
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// Person in Social domain (opaque to Sys).
        /// Sys does NOT know Person's structure - just the ID.
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// When this link was established
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Optional: Link creator (for audit)
        /// </summary>
        public string? CreatedBy { get; set; }
    }
}
