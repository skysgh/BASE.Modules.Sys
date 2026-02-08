using App.Modules.Sys.Domain.ReferenceData;
using App.Modules.Sys.Shared.Repositories;

namespace App.Modules.Sys.Domain.ReferenceData.Repositories;

/// <summary>
/// Repository interface for SystemLanguage reference data.
/// Domain layer defines repository contracts.
/// Infrastructure layer provides implementations.
/// 
/// Design Notes:
/// - Uses IQueryable for OData support and deferred execution
/// - Async methods for actual database operations
/// - Workspace-level queries for multi-tenant language availability
/// </summary>
public interface ISystemLanguageRepository : IHasRepository
{
    #region IQueryable Methods (for OData/filtering)

    /// <summary>
    /// Get all languages as IQueryable for OData support.
    /// </summary>
    /// <param name="activeOnly">If true, only return active languages.</param>
    /// <returns>Queryable of all languages.</returns>
    IQueryable<SystemLanguage> GetAllQueryable(bool activeOnly = false);

    /// <summary>
    /// Get languages for a specific workspace as IQueryable.
    /// </summary>
    /// <param name="workspaceId">The workspace ID.</param>
    /// <returns>Queryable of languages enabled for the workspace.</returns>
    IQueryable<SystemLanguage> GetForWorkspaceQueryable(Guid workspaceId);

    #endregion

    #region Async Methods (for standard operations)

    /// <summary>
    /// Get all languages.
    /// </summary>
    /// <param name="activeOnly">If true, only return active languages.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>All languages ordered by SortOrder.</returns>
    Task<IReadOnlyList<SystemLanguage>> GetAllAsync(bool activeOnly = false, CancellationToken ct = default);

    /// <summary>
    /// Get language by code (e.g., "en", "es").
    /// </summary>
    /// <param name="code">ISO 639-1 language code.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Language if found, null otherwise.</returns>
    Task<SystemLanguage?> GetByCodeAsync(string code, CancellationToken ct = default);

    /// <summary>
    /// Get the default language.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Default language (should always exist).</returns>
    Task<SystemLanguage> GetDefaultAsync(CancellationToken ct = default);

    /// <summary>
    /// Check if language code exists.
    /// </summary>
    /// <param name="code">ISO 639-1 language code.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>True if exists.</returns>
    Task<bool> ExistsAsync(string code, CancellationToken ct = default);

    /// <summary>
    /// Get languages enabled for a specific workspace.
    /// </summary>
    /// <param name="workspaceId">The workspace ID.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Languages enabled for the workspace, ordered by SortOrder.</returns>
    Task<IReadOnlyList<SystemLanguage>> GetForWorkspaceAsync(Guid workspaceId, CancellationToken ct = default);

    /// <summary>
    /// Get the default language for a specific workspace.
    /// Falls back to system default if workspace has no default.
    /// </summary>
    /// <param name="workspaceId">The workspace ID.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Default language for the workspace.</returns>
    Task<SystemLanguage> GetDefaultForWorkspaceAsync(Guid workspaceId, CancellationToken ct = default);

    #endregion

    #region CRUD Operations

    /// <summary>
    /// Add a new language.
    /// </summary>
    /// <param name="language">The language to add.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The added language with generated ID.</returns>
    Task<SystemLanguage> AddAsync(SystemLanguage language, CancellationToken ct = default);

    /// <summary>
    /// Update an existing language.
    /// </summary>
    /// <param name="language">The language with updated values.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The updated language.</returns>
    Task<SystemLanguage> UpdateAsync(SystemLanguage language, CancellationToken ct = default);

    /// <summary>
    /// Delete a language by code.
    /// </summary>
    /// <param name="code">ISO 639-1 language code.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>True if deleted, false if not found.</returns>
    Task<bool> DeleteAsync(string code, CancellationToken ct = default);

    #endregion
}
