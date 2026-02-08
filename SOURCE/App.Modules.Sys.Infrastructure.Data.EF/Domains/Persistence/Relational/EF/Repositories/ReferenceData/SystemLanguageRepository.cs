using App.Modules.Sys.Domain.ReferenceData;
using App.Modules.Sys.Domain.ReferenceData.Repositories;
using App.Modules.Sys.Infrastructure.Domains.Diagnostics;
using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Repositories.Implementations.Base;
using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Services;
using App.Modules.Sys.Shared.Lifecycles;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Repositories.ReferenceData;

/// <summary>
/// EF Core implementation of SystemLanguage repository.
/// Inherits from RepositoryBase for DbContext access.
/// 
/// Design:
/// - IQueryable methods for OData support and deferred execution
/// - Async methods for standard operations
/// - Workspace-level queries for multi-tenant language availability
/// 
/// Property mapping (entity uses contract-based names):
/// - Key (not Code) - IHasKey
/// - Title (not Name) - IHasTitleAndDescription
/// - Enabled (not IsActive) - IHasEnabled
/// - DisplayOrderHint (not SortOrder) - IHasDisplayHints
/// - ImageUrl (not IconUrl) - IHasImageUrlNullable
/// 
/// Auto-registered via IHasScopedLifecycle marker interface.
/// </summary>
internal sealed class SystemLanguageRepository : RepositoryBase, 
    ISystemLanguageRepository, IHasScopedLifecycle
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dbProvider">Provider for scoped DbContext access</param>
    /// <param name="logger">Logger instance</param>
    public SystemLanguageRepository(IScopedDbContextProviderService dbProvider, IAppLogger logger)
        : base(dbProvider, logger)
    {
    }

    #region IQueryable Methods

    /// <inheritdoc/>
    public IQueryable<SystemLanguage> GetAllQueryable(bool activeOnly = false)
    {
        var query = Context.Set<SystemLanguage>().AsNoTracking();

        if (activeOnly)
        {
            query = query.Where(l => l.Enabled);
        }

        return query.OrderBy(l => l.DisplayOrderHint).ThenBy(l => l.Title);
    }

    /// <inheritdoc/>
    public IQueryable<SystemLanguage> GetForWorkspaceQueryable(Guid workspaceId)
    {
        // Join SystemLanguage with WorkspaceLanguageAssignment
        var assignmentSet = Context.Set<WorkspaceLanguageAssignment>();
        var languageSet = Context.Set<SystemLanguage>();

        return from assignment in assignmentSet.AsNoTracking()
               join language in languageSet.AsNoTracking()
                   on assignment.LanguageCode equals language.Key
               where assignment.WorkspaceId == workspaceId && language.Enabled
               orderby assignment.SortOrder, language.Title
               select language;
    }

    #endregion

    #region Async Methods

    /// <inheritdoc/>
    public async Task<IReadOnlyList<SystemLanguage>> GetAllAsync(bool activeOnly = false, CancellationToken ct = default)
    {
        return await GetAllQueryable(activeOnly).ToListAsync(ct);
    }

    /// <inheritdoc/>
    public async Task<SystemLanguage?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return null;
        }

        return await Context.Set<SystemLanguage>()
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Key == code, ct);
    }

    /// <inheritdoc/>
    public async Task<SystemLanguage> GetDefaultAsync(CancellationToken ct = default)
    {
        var defaultLang = await Context.Set<SystemLanguage>()
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.IsDefault, ct);

        if (defaultLang is null)
        {
            throw new InvalidOperationException("No default language configured in system. Database seed data missing.");
        }

        return defaultLang;
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(string code, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return false;
        }

        return await Context.Set<SystemLanguage>()
            .AsNoTracking()
            .AnyAsync(l => l.Key == code, ct);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<SystemLanguage>> GetForWorkspaceAsync(Guid workspaceId, CancellationToken ct = default)
    {
        return await GetForWorkspaceQueryable(workspaceId).ToListAsync(ct);
    }

    /// <inheritdoc/>
    public async Task<SystemLanguage> GetDefaultForWorkspaceAsync(Guid workspaceId, CancellationToken ct = default)
    {
        // First try to find workspace-specific default
        var assignmentSet = Context.Set<WorkspaceLanguageAssignment>();
        var languageSet = Context.Set<SystemLanguage>();

        var workspaceDefault = await (
            from assignment in assignmentSet.AsNoTracking()
            join language in languageSet.AsNoTracking()
                on assignment.LanguageCode equals language.Key
            where assignment.WorkspaceId == workspaceId && assignment.IsDefault
            select language
        ).FirstOrDefaultAsync(ct);

        // Fall back to system default
        return workspaceDefault ?? await GetDefaultAsync(ct);
    }

    #endregion

    #region CRUD Operations

    /// <inheritdoc/>
    public async Task<SystemLanguage> AddAsync(SystemLanguage language, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(language);

        if (language.Id == Guid.Empty)
        {
            language.Id = Guid.NewGuid();
        }

        // Note: No CreatedAt property on entity now - use audit timestamps if needed
        await Context.Set<SystemLanguage>().AddAsync(language, ct);
        await Context.SaveChangesAsync(ct);

        return language;
    }

    /// <inheritdoc/>
    public async Task<SystemLanguage> UpdateAsync(SystemLanguage language, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(language);

        var existing = await Context.Set<SystemLanguage>()
            .FirstOrDefaultAsync(l => l.Key == language.Key, ct)
            ?? throw new InvalidOperationException($"Language with key '{language.Key}' not found.");

        // Update properties using contract-based names
        existing.Title = language.Title;
        existing.NativeName = language.NativeName;
        existing.Description = language.Description;
        existing.ImageUrl = language.ImageUrl;
        existing.Enabled = language.Enabled;
        existing.IsDefault = language.IsDefault;
        existing.DisplayOrderHint = language.DisplayOrderHint;

        await Context.SaveChangesAsync(ct);

        return existing;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(string code, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return false;
        }

        var language = await Context.Set<SystemLanguage>()
            .FirstOrDefaultAsync(l => l.Key == code, ct);

        if (language is null)
        {
            return false;
        }

        Context.Set<SystemLanguage>().Remove(language);
        await Context.SaveChangesAsync(ct);

        return true;
    }

    #endregion
}
