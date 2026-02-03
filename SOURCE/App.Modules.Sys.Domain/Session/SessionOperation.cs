using System;

namespace App.Modules.Sys.Domain.Session
{
    /// <summary>
    /// Individual operation/request within a session.
    /// Tracks what user did, when, and from where.
    /// </summary>
    /// <remarks>
    /// Design:
    /// - IP address per operation (mobile IPs change frequently)
    /// - Rich audit trail for security and analytics
    /// - Supports both page views and API calls
    /// </remarks>
    public class SessionOperation
    {
        /// <summary>
        /// Unique operation identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Which session this operation belongs to
        /// </summary>
        public Guid SessionId { get; set; }

        /// <summary>
        /// Navigation property to Session
        /// </summary>
        public Session Session { get; set; } = null!;

        /// <summary>
        /// Type of operation.
        /// Examples: "PageView", "ApiCall", "Download", "Upload"
        /// </summary>
        public string OperationType { get; set; } = string.Empty;

        /// <summary>
        /// Resource accessed.
        /// Examples: "/products", "GET /api/users/123"
        /// </summary>
        public string Resource { get; set; } = string.Empty;

        /// <summary>
        /// HTTP method if applicable.
        /// Examples: "GET", "POST", "PUT", "DELETE"
        /// </summary>
        public string? HttpMethod { get; set; }

        /// <summary>
        /// IP address for THIS specific request.
        /// Mobile IPs change frequently - track per operation, not per session.
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// User agent string (browser/device info)
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// When this operation occurred
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// HTTP status code if applicable.
        /// Examples: 200 (OK), 404 (Not Found), 500 (Error)
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// How long operation took (milliseconds)
        /// </summary>
        public long? DurationMs { get; set; }

        /// <summary>
        /// Whether operation succeeded
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Error message if operation failed
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Optional: Request payload size (bytes)
        /// </summary>
        public long? RequestSize { get; set; }

        /// <summary>
        /// Optional: Response payload size (bytes)
        /// </summary>
        public long? ResponseSize { get; set; }
    }
}
