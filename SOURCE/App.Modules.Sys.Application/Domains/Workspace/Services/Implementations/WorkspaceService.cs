using App.Modules.Sys.Application.Domains.Context.Models.Implementations;
using App.Modules.Sys.Domain.Domains.Workspaces.Repositories;
using App.Modules.Sys.Infrastructure.Domains.Diagnostics;

namespace App.Modules.Sys.Application.Domains.Workspace.Services.Implementations;

/// <summary>
/// Implementation of workspace service.
/// Maps domain entities to DTOs and orchestrates repository calls.
/// Auto-registered via IHasScopedLifecycle on interface.
/// </summary>
internal sealed class WorkspaceService : IWorkspaceService
{
    private readonly IAppLogger<WorkspaceService> _logger;
    private readonly IWorkspaceRepository _workspaceRepository;

    public WorkspaceService(
        IAppLogger<WorkspaceService> logger,
        IWorkspaceRepository workspaceRepository)
    {
        _logger = logger;
        _workspaceRepository = workspaceRepository;
    }

    public async Task<List<WorkspaceSummaryDto>> GetUserWorkspacesAsync(string userId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Getting workspaces for user {userId}");

        if (!Guid.TryParse(userId, out var userGuid))
        {
            _logger.LogWarning($"Invalid user ID format: {userId}");
            return new List<WorkspaceSummaryDto>();
        }

        var workspaces = await _workspaceRepository.GetByUserIdAsync(userGuid, cancellationToken);

        return workspaces.Select(w =>
        {
            var member = w.Members.FirstOrDefault(m => m.UserId == userGuid);
            return new WorkspaceSummaryDto
            {
                Id = w.Id.ToString(),
                Name = w.Title,
                Role = member?.Role ?? "Member",
                IsDefault = member?.IsDefault ?? false
            };
        }).ToList();
    }

    public async Task<WorkspaceDetailsDto?> GetWorkspaceDetailsAsync(string workspaceId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Getting workspace details for {workspaceId}");

        if (!Guid.TryParse(workspaceId, out var workspaceGuid))
        {
            _logger.LogWarning($"Invalid workspace ID format: {workspaceId}");
            return null;
        }

        var workspace = await _workspaceRepository.GetByIdAsync(workspaceGuid, cancellationToken);
        if (workspace == null)
        {
            return null;
        }

        return MapToDetailsDto(workspace);
    }

    public async Task<WorkspaceDetailsDto?> GetDefaultWorkspaceAsync(string userId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Getting default workspace for user {userId}");

        if (!Guid.TryParse(userId, out var userGuid))
        {
            _logger.LogWarning($"Invalid user ID format: {userId}");
            return null;
        }

        var workspace = await _workspaceRepository.GetDefaultWorkspaceAsync(userGuid, cancellationToken);
        if (workspace == null)
        {
            return null;
        }

        return MapToDetailsDto(workspace);
    }

    private static WorkspaceDetailsDto MapToDetailsDto(Domain.Domains.Workspaces.Models.Workspace workspace)
    {
        return new WorkspaceDetailsDto
        {
            Id = workspace.Id.ToString(),
            Name = workspace.Title,
            Title = workspace.Title,
            Description = string.Empty, // TODO: Add to entity
            Branding = new WorkspaceBrandingDto
            {
                OrganizationName = workspace.OrganizationName,
                LogoUrl = workspace.LogoUrl,
                Theme = new ThemeDto
                {
                    PrimaryColor = workspace.PrimaryColor,
                    CustomCssUrl = workspace.CustomCssUrl
                }
            },
            Contact = new WorkspaceContactDto
            {
                Email = null, // TODO: Add to entity
                Phone = null,
                Address = null
            },
            Account = new AccountInfoDto
            {
                Tier = workspace.SubscriptionTier,
                MaxUsers = workspace.MaxUsers,
                StorageLimitGb = workspace.StorageLimitGb,
                IsActive = workspace.IsActive
            },
            Resources = new WorkspaceResourcesDto
            {
                ImagesRoot = null, // TODO: Add to entity
                TrustedByPath = null,
                I18nPath = null
            },
            AccountFeatures = new Dictionary<string, bool>(), // TODO: Parse from entity
            UIOptions = new Dictionary<string, object>() // TODO: Parse from SettingsJson
        };
    }

    private static Dictionary<string, object> ParseSettingsJson(string? settingsJson)
    {
        if (string.IsNullOrWhiteSpace(settingsJson))
        {
            return new Dictionary<string, object>();
        }

        try
        {
            // TODO: Use System.Text.Json to deserialize
            return new Dictionary<string, object>();
        }
        catch
        {
            return new Dictionary<string, object>();
        }
    }
}
