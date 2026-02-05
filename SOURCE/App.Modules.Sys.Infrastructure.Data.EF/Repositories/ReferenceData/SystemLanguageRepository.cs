using App.Modules.Sys.Domain.ReferenceData;
using App.Modules.Sys.Domain.ReferenceData.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Infrastructure.Data.EF.Repositories.ReferenceData;

/// <summary>
/// EF Core implementation of SystemLanguage repository.
/// </summary>
internal sealed class SystemLanguageRepository : ISystemLanguageRepository
{
    private readonly DbContext _context;

    public SystemLanguageRepository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<SystemLanguage>> GetAllAsync(bool activeOnly = false, CancellationToken ct = default)
    {
        var query = _context.Set<SystemLanguage>().AsNoTracking();

        if (activeOnly)
        {
            query = query.Where(l => l.IsActive);
        }

        return await query
            .OrderBy(l => l.SortOrder)
            .ThenBy(l => l.Name)
            .ToListAsync(ct);
    }

    /// <inheritdoc/>
    public async Task<SystemLanguage?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return null;
        }

        return await _context.Set<SystemLanguage>()
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.Code.Equals(code, StringComparison.OrdinalIgnoreCase), ct);
    }

    /// <inheritdoc/>
    public async Task<SystemLanguage> GetDefaultAsync(CancellationToken ct = default)
    {
        var defaultLang = await _context.Set<SystemLanguage>()
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.IsDefault, ct);

        if (defaultLang == null)
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

        return await _context.Set<SystemLanguage>()
            .AsNoTracking()
            .AnyAsync(l => l.Code.Equals(code, StringComparison.OrdinalIgnoreCase), ct);
    }
}
