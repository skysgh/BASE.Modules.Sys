using System;

namespace App.Modules.Sys.Interfaces.Models.Session
{
    /// <summary>
    /// DTO for SessionOperation (API contract).
    /// Individual request/operation within a session.
    /// </summary>
    public class SessionOperationDto
    {
        /// <summary>
        /// Operation ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Session ID this operation belongs to
        /// </summary>
        public Guid SessionId { get; set; }

        /// <summary>
        /// Type of operation (PageView, ApiCall, etc.)
        /// </summary>
        public string OperationType { get; set; } = string.Empty;

        /// <summary>
        /// Resource accessed (URL, endpoint)
        /// </summary>
        public string Resource { get; set; } = string.Empty;

        /// <summary>
        /// HTTP method (GET, POST, etc.)
        /// </summary>
        public string? HttpMethod { get; set; }

        /// <summary>
        /// IP address for this request
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// When operation occurred
        /// </summary>
        public string Timestamp { get; set; } = string.Empty;

        /// <summary>
        /// HTTP status code
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// Duration in milliseconds
        /// </summary>
        public long? DurationMs { get; set; }

        /// <summary>
        /// Whether operation succeeded
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Error message if failed
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
}
