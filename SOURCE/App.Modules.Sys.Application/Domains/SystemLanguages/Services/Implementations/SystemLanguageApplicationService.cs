using App.Modules.Sys.Application.ReferenceData.Models;
using App.Modules.Sys.Application.ReferenceData.Services;
using App.Modules.Sys.Domain.ReferenceData;
using App.Modules.Sys.Domain.ReferenceData.Repositories;
using App.Modules.Sys.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Application.ReferenceData.Services.Implementations;

/// <summary>
/// Application service implementation for SystemLanguage reference data.
/// Delegates to repository and maps to DTOs.
/// </summary>
internal sealed class SystemLanguageApplicationService : ISystemLanguageApplicationService
{
    private readonly ISystemLanguageRepository _repository;
    private readonly IObjectMappingService _mapper;

    public SystemLanguageApplicationService(
        ISystemLanguageRepository repository,
        IObjectMappingService mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <inheritdoc/>
    public IQueryable<SystemLanguageDto> GetLanguagesQueryable(bool activeOnly = false)
    {
        // For OData support - uses abstracted ProjectTo (hides AutoMapper)
        var query = _repository.GetAllAsync(activeOnly).GetAwaiter().GetResult().AsQueryable();
        
        // Use mapping service abstraction (no AutoMapper dependency in Application layer)
        return _mapper.ProjectTo<SystemLanguage, SystemLanguageDto>(query);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<SystemLanguageDto>> GetAllLanguagesAsync(bool activeOnly = false, CancellationToken ct = default)
    {
        var languages = await _repository.GetAllAsync(activeOnly, ct);
        
        return languages
            .Select(lang => _mapper.Map<SystemLanguage, SystemLanguageDto>(lang))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<SystemLanguageDto?> GetLanguageByCodeAsync(string code, CancellationToken ct = default)
    {
        var language = await _repository.GetByCodeAsync(code, ct);
        
        return language != null 
            ? _mapper.Map<SystemLanguage, SystemLanguageDto>(language)
            : null;
    }

    /// <inheritdoc/>
    public async Task<SystemLanguageDto> GetDefaultLanguageAsync(CancellationToken ct = default)
    {
        var language = await _repository.GetDefaultAsync(ct);
        
        return _mapper.Map<SystemLanguage, SystemLanguageDto>(language);
    }
}
