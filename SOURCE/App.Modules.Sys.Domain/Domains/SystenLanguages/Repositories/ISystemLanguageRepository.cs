using App.Modules.Sys.Domain.ReferenceData;
using App.Modules.Sys.Shared.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Domain.ReferenceData.Repositories;

/// <summary>
/// Repository interface for SystemLanguage reference data.
/// Domain layer defines repository contracts.
/// Infrastructure layer provides implementations.
/// </summary>
public interface ISystemLanguageRepository : IHasRepository
{
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
}
