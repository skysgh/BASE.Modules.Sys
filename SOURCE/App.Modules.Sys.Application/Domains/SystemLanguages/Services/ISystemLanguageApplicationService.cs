using App.Modules.Sys.Application.ReferenceData.Models;
using App.Modules.Sys.Shared.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Application.ReferenceData.Services;

/// <summary>
/// Application service for SystemLanguage reference data.
/// Returns DTOs for API consumption.
/// </summary>
public interface ISystemLanguageApplicationService : IHasService
{
    /// <summary>
    /// Get all system languages as queryable (for OData support).
    /// </summary>
    /// <param name="activeOnly">If true, only active languages.</param>
    /// <returns>Queryable collection of language DTOs.</returns>
    IQueryable<SystemLanguageDto> GetLanguagesQueryable(bool activeOnly = false);

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
    /// Get the default language.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>Default language DTO.</returns>
    Task<SystemLanguageDto> GetDefaultLanguageAsync(CancellationToken ct = default);
}
