using App.Modules.Sys.Application.Domains.Context.Models.Implementations;
using App.Modules.Sys.Application.Domains.Context.Services;
using App.Modules.Sys.Application.ReferenceData.Models;
using App.Modules.Sys.Application.ReferenceData.Services;
using App.Modules.Sys.Shared.Lifecycles;
using App.Modules.Sys.Substrate.Contracts.Social;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Application.Domains.Context.Implementations;

/// <summary>
/// Implementation of ApplicationContextService.
/// Composes context from various providers (system, workspace, user, settings).
/// </summary>
public class ApplicationContextService : IApplicationContextService, IHasScopedLifecycle
{
    private readonly IPersonIdentityResolver _identityResolver;
    private readonly ISystemContextProvider _systemProvider;
    private readonly ISystemLanguageApplicationService? _languageService;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ApplicationContextService(
        IPersonIdentityResolver identityResolver,
        ISystemContextProvider systemProvider,
        ISystemLanguageApplicationService? languageService = null)
    {
        this._identityResolver = identityResolver;
        this._systemProvider = systemProvider;
        this._languageService = languageService;
    }

    /// <inheritdoc/>
    public async Task<ApplicationContextDto> GetApplicationContextAsync(CancellationToken cancellationToken = default)
    {
        // Get current user's identity (works in both lite and full mode)
        var userIdentity = await this._identityResolver.GetCurrentUserIdentityAsync(cancellationToken);
        
        // Build user context from identity (null if not authenticated)
        UserContextDto? userContext = null;
        var isAuthenticated = userIdentity != null;
        
        if (userIdentity != null)
        {
            userContext = new UserContextDto
            {
                Id = userIdentity.PersonId.ToString(),
                IsAnonymous = false,
                DisplayName = userIdentity.DisplayName ?? "User",
                Email = userIdentity.Email,
                ProfileImageUrl = userIdentity.ProfileImageUrl,
                IsLiteMode = userIdentity.IsLiteMode
            };
        }
        
        // Get system context from provider (database or stub)
        var systemContext = await this._systemProvider.GetSystemContextAsync(cancellationToken);

        // Get languages from database if available
        var languages = await this.GetLanguagesAsync(cancellationToken);
        
        // Build the complete context
        var context = new ApplicationContextDto
        {
            System = systemContext,
            
            // TODO: Wire up from WorkspaceResolutionMiddleware + WorkspaceService
            Workspace = await GetWorkspaceContextStubAsync(),
            
            User = userContext,
            
            Session = new SessionContextDto
            {
                Id = Guid.NewGuid(), // TODO: Get from ISessionService
                IsAuthenticated = isAuthenticated,
                ExpiresAt = DateTime.UtcNow.AddHours(8),
                LastActivityAt = DateTime.UtcNow
            },
            
            // TODO: Wire up from INavigationContextProvider
            Navigation = GetNavigationStub(),
            
            // TODO: Wire up from ISettingsContextProvider  
            Settings = new ComputedSettingsDto
            {
                Theme = "light",
                Language = "en"
            },
            
            AvailableLanguages = languages,
            
            GeneratedAt = DateTime.UtcNow
        };

        return context;
    }

    private async Task<List<SystemLanguageDto>> GetLanguagesAsync(CancellationToken ct)
    {
        if (this._languageService != null)
        {
            try
            {
                var languages = await this._languageService.GetAllLanguagesAsync(activeOnly: true, ct);
                return languages.ToList();
            }
            catch
            {
                // Fall back to stub if service fails
            }
        }

        // Stub fallback
        return new List<SystemLanguageDto>
        {
            new() { Code = "en", Name = "English", NativeName = "English", Description = "English", IconUrl = "/assets/core/deployed/images/flags/nz.svg", IsActive = true, IsDefault = true },
            new() { Code = "mi", Name = "Te Reo Māori", NativeName = "Te Reo Māori", Description = "New Zealand Māori", IconUrl = "/assets/core/deployed/images/flags/mi.svg", IsActive = true, IsDefault = false }
        };
    }

    private static async Task<WorkspaceContextDto> GetWorkspaceContextStubAsync()
    {
        // TODO: Replace with real workspace service
        await Task.CompletedTask;
        
        return new WorkspaceContextDto
        {
            MemberOf = new List<WorkspaceSummaryDto>
            {
                new() { Id = "default", Name = "Default Workspace", Role = "Owner", IsDefault = true }
            },
            Current = new WorkspaceDetailsDto
            {
                Id = "default",
                Name = "Default Workspace",
                Title = "Default Workspace",
                Description = "System default workspace",
                Branding = new WorkspaceBrandingDto
                {
                    OrganizationName = "BASE Platform",
                    LogoUrl = "/assets/system/logo.png",
                    Theme = new ThemeDto
                    {
                        PrimaryColor = "#0078D4",
                        SecondaryColor = "#4ECDC4",
                        AccentColor = "#FFE66D"
                    }
                },
                Contact = new WorkspaceContactDto
                {
                    Email = "support@base-software.com"
                },
                Resources = new WorkspaceResourcesDto
                {
                    ImagesRoot = "/assets/",
                    I18nPath = "/assets/i18n"
                },
                AccountFeatures = new Dictionary<string, bool>
                {
                    { "enableAnalytics", true }
                },
                UIOptions = new Dictionary<string, object>
                {
                    { "showToolbar", true }
                }
            }
        };
    }

    private static NavigationContextDto GetNavigationStub()
    {
        // TODO: Replace with INavigationContextProvider
        return new NavigationContextDto
        {
            CurrentRoute = "/",
            Breadcrumbs = new List<BreadcrumbDto>
            {
                new() { Label = "Home", Route = "/", IsCurrent = true }
            },
            PrimaryMenu = new List<NavigationItemDto>
            {
                new() { Id = "dashboard", Label = "Dashboard", Route = "/dashboard", Icon = "home", IsActive = false },
                new() { Id = "work", Label = "Work Items", Route = "/work", Icon = "list", IsActive = false, BadgeCount = 0 },
                new() { Id = "settings", Label = "Settings", Route = "/settings", Icon = "settings", IsActive = false }
            }
        };
    }
}
