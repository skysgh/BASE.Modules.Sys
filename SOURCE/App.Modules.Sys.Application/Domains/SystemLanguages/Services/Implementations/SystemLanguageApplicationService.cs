using App.Modules.Sys.Application.ReferenceData.Models;
using App.Modules.Sys.Application.ReferenceData.Services;
using App.Modules.Sys.Domain.ReferenceData;
using App.Modules.Sys.Domain.ReferenceData.Repositories;
using App.Modules.Sys.Infrastructure.Services;

namespace App.Modules.Sys.Application.ReferenceData.Services.Implementations;

/// <summary>
/// Application service implementation for SystemLanguage reference data.
/// 
/// Architecture:
/// - Delegates to repository for data access
/// - Maps domain entities to DTOs
/// - Supports IQueryable for OData filtering
/// - Handles workspace-level language assignments
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

    #region IQueryable Methods

    /// <inheritdoc/>
    public IQueryable<SystemLanguageDto> GetLanguagesQueryable(bool activeOnly = false)
    {
        var query = _repository.GetAllQueryable(activeOnly);
        return _mapper.ProjectTo<SystemLanguage, SystemLanguageDto>(query);
    }

    /// <inheritdoc/>
    public IQueryable<SystemLanguageDto> GetWorkspaceLanguagesQueryable(Guid workspaceId)
    {
        var query = _repository.GetForWorkspaceQueryable(workspaceId);
        return _mapper.ProjectTo<SystemLanguage, SystemLanguageDto>(query);
    }

    #endregion

    #region Standard Queries

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
        
        return language is not null 
            ? _mapper.Map<SystemLanguage, SystemLanguageDto>(language)
            : null;
    }

    /// <inheritdoc/>
    public async Task<SystemLanguageDto> GetDefaultLanguageAsync(CancellationToken ct = default)
    {
        var language = await _repository.GetDefaultAsync(ct);
        return _mapper.Map<SystemLanguage, SystemLanguageDto>(language);
    }

    #endregion

    #region Workspace-Level Queries

    /// <inheritdoc/>
    public async Task<IReadOnlyList<SystemLanguageDto>> GetWorkspaceLanguagesAsync(Guid workspaceId, CancellationToken ct = default)
    {
        var languages = await _repository.GetForWorkspaceAsync(workspaceId, ct);
        
        return languages
            .Select(lang => _mapper.Map<SystemLanguage, SystemLanguageDto>(lang))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<SystemLanguageDto> GetWorkspaceDefaultLanguageAsync(Guid workspaceId, CancellationToken ct = default)
    {
        var language = await _repository.GetDefaultForWorkspaceAsync(workspaceId, ct);
        return _mapper.Map<SystemLanguage, SystemLanguageDto>(language);
    }

    /// <inheritdoc/>
    public async Task SetWorkspaceLanguagesAsync(Guid workspaceId, IEnumerable<string> languageCodes, string defaultLanguageCode, CancellationToken ct = default)
    {
        // TODO: Implement when WorkspaceLanguageAssignmentRepository is created
        // 1. Delete existing assignments for workspaceId
        // 2. Create new assignments for each languageCode
        // 3. Set IsDefault = true for defaultLanguageCode
        await Task.CompletedTask;
        throw new NotImplementedException("WorkspaceLanguageAssignment repository not yet implemented");
    }

    #endregion

    #region CRUD Operations

    /// <inheritdoc/>
    public async Task<SystemLanguageDto> CreateLanguageAsync(CreateSystemLanguageDto dto, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        // Validate code doesn't exist
        if (await _repository.ExistsAsync(dto.Code, ct))
        {
            throw new InvalidOperationException($"Language with code '{dto.Code}' already exists.");
        }

        var language = new SystemLanguage
        {
            Id = Guid.NewGuid(),
            Key = dto.Code.ToLowerInvariant(),
            Title = dto.Name,
            NativeName = dto.NativeName,
            Description = dto.Description ?? string.Empty,
            ImageUrl = dto.IconUrl,
            Enabled = dto.IsActive,
            IsDefault = dto.IsDefault,
            DisplayOrderHint = dto.SortOrder
        };

        var created = await _repository.AddAsync(language, ct);
        return _mapper.Map<SystemLanguage, SystemLanguageDto>(created);
    }

    /// <inheritdoc/>
    public async Task<SystemLanguageDto> UpdateLanguageAsync(string code, UpdateSystemLanguageDto dto, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var existing = await _repository.GetByCodeAsync(code, ct)
            ?? throw new InvalidOperationException($"Language with code '{code}' not found.");

        // Apply updates (only non-null values)
        if (dto.Name is not null)
        {
            existing.Title = dto.Name;
        }
        if (dto.NativeName is not null)
        {
            existing.NativeName = dto.NativeName;
        }
        if (dto.Description is not null)
        {
            existing.Description = dto.Description;
        }
        if (dto.IconUrl is not null)
        {
            existing.ImageUrl = dto.IconUrl;
        }
        if (dto.IsActive.HasValue)
        {
            existing.Enabled = dto.IsActive.Value;
        }
        if (dto.IsDefault.HasValue)
        {
            existing.IsDefault = dto.IsDefault.Value;
        }
        if (dto.SortOrder.HasValue)
        {
            existing.DisplayOrderHint = dto.SortOrder.Value;
        }

        var updated = await _repository.UpdateAsync(existing, ct);
        return _mapper.Map<SystemLanguage, SystemLanguageDto>(updated);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteLanguageAsync(string code, CancellationToken ct = default)
    {
        return await _repository.DeleteAsync(code, ct);
    }

    #endregion
}
