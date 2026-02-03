using System;

namespace App.Modules.Sys.Interfaces.Models.Session
{
    /// <summary>
    /// DTO for Session (API contract).
    /// Lightweight, serialization-friendly.
    /// </summary>
    public class SessionDto
    {
        /// <summary>
        /// Session ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User ID (null if anonymous)
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Whether session has authenticated user
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// When session was created
        /// </summary>
        public string CreatedAt { get; set; } = string.Empty;

        /// <summary>
        /// Last activity timestamp
        /// </summary>
        public string? LastActivityAt { get; set; }

        /// <summary>
        /// When session expires
        /// </summary>
        public string? ExpiresAt { get; set; }

        /// <summary>
        /// Whether session is currently active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Number of operations in this session
        /// </summary>
        public int OperationCount { get; set; }
    }
}
