using App.Modules.Sys.Application.Domains.Context.Models.Implementations;
using App.Modules.Sys.Application.Domains.Users.Profiles.Preferences;
using App.Modules.Sys.Application.Domains.Workspace.Services;
using App.Modules.Sys.Infrastructure.Domains.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace App.Modules.Sys.Application.Domains.Context.Services.Implementations;

/// <summary>
/// Implementation of application context service (orchestrator).
/// Aggregates system, workspace, user, and computed settings into a single context.
/// Auto-registered via IHasScopedLifecycle on interface.
/// </summary>
internal sealed class ContextService : IApplicationContextService
{
    private readonly IAppLogger<ContextService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IUserProfileService _userProfileService;
    private readonly IWorkspaceService _workspaceService;
    // TODO: Add ICurrentUserService to get current user from claims

    public ContextService(
        IAppLogger<ContextService> logger,
        IConfiguration configuration,
        IUserProfileService userProfileService,
        IWorkspaceService workspaceService)
    {
        _logger = logger;
        _configuration = configuration;
        _userProfileService = userProfileService;
        _workspaceService = workspaceService;
    }

    public async Task<ApplicationContextDto> GetApplicationContextAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Building application context");

        // Build system context
        var systemContext = BuildSystemContext();

        // Build workspace context (if applicable)
        var workspaceContext = await BuildWorkspaceContextAsync(null, cancellationToken);

        // Build user context
        // TODO: Get current user from ICurrentUserService
        var userContext = await BuildUserContextAsync(null, cancellationToken);

        // Compute merged settings
        var computedSettings = ComputeSettings(systemContext, workspaceContext, userContext);

        return new ApplicationContextDto
        {
            System = systemContext,
            Workspace = workspaceContext,
            User = userContext,
            Settings = computedSettings,
            GeneratedAt = DateTime.UtcNow
        };
    }

    private SystemContextDto BuildSystemContext()
    {
        return new SystemContextDto
        {
            Name = _configuration["App:Name"] ?? "BASE Platform",
            Version = _configuration["App:Version"] ?? "1.0.0",
            Environment = _configuration["App:Environment"] ?? "Development",
            Branding = new SystemBrandingDto
            {
                Provider = new ProviderInfoDto
                {
                    Name = _configuration["App:Provider:Name"] ?? "BASE Provider",
                    ContactEmail = _configuration["App:Provider:Email"] ?? "support@base.example.com"
                },
                Developer = new DeveloperInfoDto
                {
                    Name = _configuration["App:Developer:Name"] ?? "BASE Development Team",
                    ContactEmail = _configuration["App:Developer:Email"] ?? "dev@base.example.com"
                }
            },
            Features = new Dictionary<string, bool>
            {
                ["multiTenancy"] = true,
                ["darkMode"] = true,
                ["notifications"] = true
            }
        };
    }

    private async Task<WorkspaceContextDto?> BuildWorkspaceContextAsync(ClaimsPrincipal? user, CancellationToken cancellationToken)
    {
        if (user?.Identity?.IsAuthenticated != true)
        {
            // Anonymous users don't have workspace context
            return null;
        }

        // TODO: Get user ID from ICurrentUserService
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return null;
        }

        // Get all workspaces user belongs to
        var memberOf = await _workspaceService.GetUserWorkspacesAsync(userId, cancellationToken);

        // Get current/default workspace
        var current = await _workspaceService.GetDefaultWorkspaceAsync(userId, cancellationToken);

        // If no default, use first workspace
        if (current == null && memberOf.Count > 0)
        {
            current = await _workspaceService.GetWorkspaceDetailsAsync(memberOf[0].Id, cancellationToken);
        }

        return new WorkspaceContextDto
        {
            MemberOf = memberOf,
            Current = current
        };
    }

    private async Task<UserContextDto?> BuildUserContextAsync(ClaimsPrincipal? user, CancellationToken cancellationToken)
    {
        if (user?.Identity?.IsAuthenticated != true)
        {
            // Return anonymous user context
            return new UserContextDto
            {
                Id = "anonymous",
                IsAnonymous = true
            };
        }

        // TODO: Use proper claim extraction via ICurrentUserService
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "unknown";
        
        // Get system profile (preferences)
        var systemProfile = await _userProfileService.GetSystemProfileAsync(userId, cancellationToken);

        // TODO: Get security profile from Security module when available
        var securityProfile = new SecurityProfileDto
        {
            Roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList(),
            Permissions = new Dictionary<string, bool>
            {
                ["canRead"] = true,
                ["canWrite"] = true
            }
        };

        return new UserContextDto
        {
            Id = userId,
            IsAnonymous = false,
            DisplayName = user.FindFirst(ClaimTypes.Name)?.Value,
            Email = user.FindFirst(ClaimTypes.Email)?.Value,
            Profiles = new UserProfilesDto
            {
                System = systemProfile ?? new SystemProfileDto(),
                Security = securityProfile
            },
            Notifications = new UserNotificationsDto
            {
                UnreadCount = 0,
                PendingTasksCount = 0
            }
        };
    }

    private static ComputedSettingsDto ComputeSettings(
        SystemContextDto system,
        WorkspaceContextDto? workspace,
        UserContextDto? user)
    {
        // Settings hierarchy: User > Workspace > System
        var systemProfile = user?.Profiles.System;

        return new ComputedSettingsDto
        {
            Language = systemProfile?.Language ?? "en-US",
            Timezone = systemProfile?.Timezone ?? "UTC",
            Theme = systemProfile?.Theme ?? "Auto",
            DateFormat = systemProfile?.DateFormat ?? "MM/DD/YYYY",
            TimeFormat = systemProfile?.TimeFormat ?? "12h",
            Branding = new EffectiveBrandingDto
            {
                Name = workspace?.Current?.Branding?.OrganizationName ?? system.Name,
                LogoUrl = workspace?.Current?.Branding?.LogoUrl,
                PrimaryColor = workspace?.Current?.Branding?.PrimaryColor,
                CustomCssUrl = workspace?.Current?.Branding?.CustomCssUrl
            }
        };
    }
}


