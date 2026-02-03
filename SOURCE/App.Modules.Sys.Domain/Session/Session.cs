using App.Modules.Sys.Domain.Identity;
using System;
using System.Collections.Generic;

namespace App.Modules.Sys.Domain.Session
{
    /// <summary>
    /// Tracks user session across multiple requests.
    /// Supports both anonymous and authenticated sessions.
    /// </summary>
    /// <remarks>
    /// Design:
    /// - UserId nullable: null = anonymous, populated = authenticated
    /// - Each session unique (no shared "anonymous user")
    /// - Clean transition: anonymous ? authenticated
    /// - Audit trail preserved throughout journey
    /// </remarks>
    public class Session
    {
        /// <summary>
        /// Unique session identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User associated with this session.
        /// NULL = Anonymous (not yet authenticated)
        /// Populated = Authenticated user
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Navigation property to User (null for anonymous)
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Session token (for cookie/JWT correlation)
        /// </summary>
        public string SessionToken { get; set; } = string.Empty;

        /// <summary>
        /// When session was created
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Last activity timestamp (updated on each request)
        /// </summary>
        public DateTime? LastActivityAt { get; set; }

        /// <summary>
        /// When session expires (sliding or absolute)
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// When user explicitly logged out
        /// </summary>
        public DateTime? TerminatedAt { get; set; }

        // Navigation properties

        /// <summary>
        /// All operations performed in this session
        /// </summary>
        public List<SessionOperation> Operations { get; set; } = new();

        // Helper properties

        /// <summary>
        /// Whether session has authenticated user
        /// </summary>
        public bool IsAuthenticated => UserId.HasValue;

        /// <summary>
        /// Whether session has expired
        /// </summary>
        public bool IsExpired => ExpiresAt.HasValue && ExpiresAt < DateTime.UtcNow;

        /// <summary>
        /// Whether session is currently active
        /// </summary>
        public bool IsActive => !IsExpired && !TerminatedAt.HasValue;
    }
}
