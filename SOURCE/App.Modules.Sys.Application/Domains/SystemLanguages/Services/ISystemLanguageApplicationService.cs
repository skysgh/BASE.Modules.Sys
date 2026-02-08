using App.Modules.Sys.Application.ReferenceData.Models;
using App.Modules.Sys.Shared.Services;

namespace App.Modules.Sys.Application.ReferenceData.Services;

/// <summary>
/// Application service for SystemLanguage reference data.
/// Returns DTOs for API consumption.
/// 
/// Architecture:
/// - IQueryable methods for OData support and admin UI filtering
/// - Async methods for standard operations
/// - Workspace-level queries for multi-tenant language availability
/// - CRUD operations for admin management
/// </summary>
public interface ISystemLanguageApplicationService : IHasService
{
    #region IQueryable Methods (OData/Admin UI)

    /// <summary>
    /// Get all system languages as queryable (for OData support).
    /// Admin use: View and filter ALL languages in the system.
    /// </summary>
    /// <param name="activeOnly">If true, only active languages.</param>
    /// <returns>Queryable collection of language DTOs.</returns>
    IQueryable<SystemLanguageDto> GetLanguagesQueryable(bool activeOnly = false);

    /// <summary>
    /// Get languages for a specific workspace as queryable.
    /// </summary>
    /// <param name="workspaceId">The workspace ID.</param>
    /// <returns>Queryable of languages enabled for the workspace.</returns>
    IQueryable<SystemLanguageDto> GetWorkspaceLanguagesQueryable(Guid workspaceId);

    #endregion

    #region Standard Queries

    /// <summary>
    /// Get all system languages.
    /// </summary>
    /// <param name="activeOnly">If true, only active languages.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>List of language DTOs.</returns>
    Task<IReadOnlyList<SystemLanguageDto>> GetAllLanguagesAsync(bool activeOnly = false, CancellationToken ct = default);

    /// <summary>
    /// Get language by code.
    /// </summary>
    /// <param name="code">ISO 639-1 code (e.g., "en").</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Language DTO if found, null otherwise.</returns>
    Task<SystemLanguageDto?> GetLanguageByCodeAsync(string code, CancellationToken ct = default);

    /// <summary>
    /// Get the system default language.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Default language DTO.</returns>
    Task<SystemLanguageDto> GetDefaultLanguageAsync(CancellationToken ct = default);

    #endregion

    #region Workspace-Level Queries

    /// <summary>
    /// Get languages enabled for a specific workspace.
    /// </summary>
    /// <param name="workspaceId">The workspace ID.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Languages enabled for the workspace.</returns>
    Task<IReadOnlyList<SystemLanguageDto>> GetWorkspaceLanguagesAsync(Guid workspaceId, CancellationToken ct = default);

    /// <summary>
    /// Get the default language for a specific workspace.
    /// Falls back to system default if workspace has no default.
    /// </summary>
    /// <param name="workspaceId">The workspace ID.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Default language for the workspace.</returns>
    Task<SystemLanguageDto> GetWorkspaceDefaultLanguageAsync(Guid workspaceId, CancellationToken ct = default);

    /// <summary>
    /// Assign languages to a workspace.
    /// Replaces all existing assignments.
    /// </summary>
    /// <param name="workspaceId">The workspace ID.</param>
    /// <param name="languageCodes">Language codes to enable.</param>
    /// <param name="defaultLanguageCode">The default language code for the workspace.</param>
    /// <param name="ct">Cancellation token.</param>
    Task SetWorkspaceLanguagesAsync(Guid workspaceId, IEnumerable<string> languageCodes, string defaultLanguageCode, CancellationToken ct = default);

    #endregion

    #region CRUD Operations (Admin)

    /// <summary>
    /// Create a new system language.
    /// </summary>
    /// <param name="dto">Language data.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Created language DTO.</returns>
    Task<SystemLanguageDto> CreateLanguageAsync(CreateSystemLanguageDto dto, CancellationToken ct = default);

    /// <summary>
    /// Update an existing system language.
    /// </summary>
    /// <param name="code">Language code to update.</param>
    /// <param name="dto">Updated language data.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Updated language DTO.</returns>
    Task<SystemLanguageDto> UpdateLanguageAsync(string code, UpdateSystemLanguageDto dto, CancellationToken ct = default);

    /// <summary>
    /// Delete a system language.
    /// </summary>
    /// <param name="code">Language code to delete.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>True if deleted, false if not found.</returns>
    Task<bool> DeleteLanguageAsync(string code, CancellationToken ct = default);

    #endregion
}

/// <summary>
/// DTO for creating a new system language.
/// </summary>
public record CreateSystemLanguageDto(
    string Code,
    string Name,
    string? NativeName,
    string? Description,
    string? IconUrl,
    bool IsActive = true,
    bool IsDefault = false,
    int SortOrder = 0
);

/// <summary>
/// DTO for updating an existing system language.
/// </summary>
public record UpdateSystemLanguageDto(
    string? Name,
    string? NativeName,
    string? Description,
    string? IconUrl,
    bool? IsActive,
    bool? IsDefault,
    int? SortOrder
);
