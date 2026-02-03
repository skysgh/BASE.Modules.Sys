using App.Modules.Sys.Interfaces.Models.Session;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Application.Services.Session
{
    /// <summary>
    /// Service for managing user sessions.
    /// Handles session lifecycle and querying.
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// Get sessions with pagination and filtering.
        /// </summary>
        Task<IEnumerable<SessionDto>> GetSessionsAsync(
            int skip = 0,
            int take = 50,
            bool activeOnly = false,
            CancellationToken ct = default);

        /// <summary>
        /// Get session by ID.
        /// </summary>
        Task<SessionDto?> GetSessionByIdAsync(
            Guid id,
            CancellationToken ct = default);

        /// <summary>
        /// Get operations for a session.
        /// </summary>
        Task<IEnumerable<SessionOperationDto>> GetSessionOperationsAsync(
            Guid sessionId,
            int skip = 0,
            int take = 50,
            CancellationToken ct = default);

        /// <summary>
        /// Check if session exists.
        /// </summary>
        Task<bool> SessionExistsAsync(
            Guid id,
            CancellationToken ct = default);
    }
}
