using App.Modules.Sys.Application.Domains.Context.Models.Implementations;
using App.Modules.Sys.Application.ReferenceData.Models;
using App.Modules.Sys.Shared.Lifecycles;
using App.Modules.Sys.Substrate.Contracts.Social;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Application.Domains.Context.Implementations;

/// <summary>
/// Implementation of ApplicationContextService.
/// Returns context data including user identity from IPersonIdentityResolver.
/// </summary>
public class ApplicationContextService : IApplicationContextService, IHasSingletonLifecycle
{
    private readonly IPersonIdentityResolver _identityResolver;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="identityResolver">Person identity resolver (lite or full mode).</param>
    public ApplicationContextService(IPersonIdentityResolver identityResolver)
    {
        this._identityResolver = identityResolver;
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
                DisplayName = userIdentity.DisplayName ?? "User",
                Email = userIdentity.Email,
                ProfileImageUrl = userIdentity.ProfileImageUrl,
                IsLiteMode = userIdentity.IsLiteMode
            };
        }
        
        // TODO: Replace with real data from database
        // Values marked with '!' are SERVER stubs (not client hardcoded)
        
        var context = new ApplicationContextDto
        {
            System = new SystemContextDto
            {
                Name = "BASE Platform!",  // '!' = from server
                Version = "1.0.0!",
                Environment = "Development!",
                Branding = new SystemBrandingDto
                {
                    Provider = new ProviderInfoDto
                    {
                        Name = "BASE Software Inc!",
                        ContactEmail = "support@base-software.com!",
                        LogoUrl = "/assets/system/logo.png!",
                        SupportUrl = "https://support.base-software.com!",
                        LegalUrl = "https://base-software.com/legal!"
                    },
                    Developer = new DeveloperInfoDto
                    {
                        Name = "BASE Development Team!",
                        ContactEmail = "dev@base-software.com!",
                        DocsUrl = "https://docs.base-software.com!"
                    }
                },
                Features = new Dictionary<string, bool>
                {
                    { "enableMultiTenant", true },
                    { "enableAdvancedSettings", true }
                }
            },
            
            // TODO: Wire up from WorkspaceResolutionMiddleware
            // Data structure matches CLIENT foo/bar tenant configs
            Workspace = new WorkspaceContextDto
            {
                MemberOf = new List<WorkspaceSummaryDto>
                {
                    new() { Id = "foo!", Name = "Foo Corporation!", Role = "Owner!", IsDefault = true },
                    new() { Id = "bar!", Name = "Bar Industries!", Role = "Member!", IsDefault = false }
                },
                Current = new WorkspaceDetailsDto
                {
                    Id = "foo!",
                    Name = "Foo Corporation!",
                    Title = "Foo Corporation!",
                    Description = "Leading provider of Foo services!",
                    Branding = new WorkspaceBrandingDto
                    {
                        OrganizationName = "Foo Corporation!",
                        LogoUrl = "/assets/tenants/foo/media/logo-dark.png!",
                        LogoDarkUrl = "/assets/tenants/foo/media/logo-light.png!",
                        LogoSmallUrl = "/assets/tenants/foo/media/logo-sm.png!",
                        Theme = new ThemeDto
                        {
                            PrimaryColor = "#ff6b6b!",
                            SecondaryColor = "#4ecdc4!",
                            AccentColor = "#ffe66d!"
                        }
                    },
                    Contact = new WorkspaceContactDto
                    {
                        Email = "contact@foo.com!",
                        Phone = "+1-555-FOO-CORP!",
                        Address = new AddressDto
                        {
                            Street = "789 Foo Plaza!",
                            City = "Foo City!",
                            Region = "FC!",
                            PostalCode = "12345!",
                            Country = "USA!"
                        }
                    },
                    Resources = new WorkspaceResourcesDto
                    {
                        ImagesRoot = "/assets/tenants/foo/media/images/!",
                        TrustedByPath = "/assets/tenants/foo/media/images/trustedBy/!",
                        I18nPath = "/assets/tenants/foo!"
                    },
                    AccountFeatures = new Dictionary<string, bool>
                    {
                        { "enableAnalytics", true },
                        { "enableChat", false }
                    },
                    UIOptions = new Dictionary<string, object>
                    {
                        { "showToolbar", true },
                        { "compactMode", false }
                    }
                }
            },
            
            
            User = userContext, // From IPersonIdentityResolver (lite or full mode)
            
            Session = new SessionContextDto
            {
                Id = Guid.NewGuid(), // TODO: Get from ISessionService
                IsAuthenticated = isAuthenticated,
                ExpiresAt = DateTime.UtcNow.AddHours(8),
                LastActivityAt = DateTime.UtcNow
            },
            
            Navigation = new NavigationContextDto
            {
                CurrentRoute = "/!",
                Breadcrumbs = new List<BreadcrumbDto>
                {
                    new() { Label = "Home!", Route = "/!", IsCurrent = true }
                },
                PrimaryMenu = new List<NavigationItemDto>
                {
                    new() { Id = "dashboard!", Label = "Dashboard!", Route = "/dashboard!", Icon = "home!", IsActive = false },
                    new() { Id = "work!", Label = "Work Items!", Route = "/work!", Icon = "list!", IsActive = false, BadgeCount = 5 },
                    new() { Id = "settings!", Label = "Settings!", Route = "/settings!", Icon = "settings!", IsActive = false }
                }
            },
            
            Settings = new ComputedSettingsDto
            {
                Theme = "light!",
                Language = "en-US!"
            },
            
            // Available system languages (enabled only)
            // Matches CLIENT json-server data: base_service_Languages (enabled=true)
            // '!' suffix on display strings only (NOT on code or iconUrl - those are functional!)
            AvailableLanguages = new List<SystemLanguageDto>
            {
                new() { Code = "en", Name = "NZ English!", NativeName = "Kiwi!", Description = "Kiwi!", IconUrl = "/assets/core/deployed/images/flags/nz.svg", IsActive = true, IsDefault = true },
                new() { Code = "mi", Name = "Te Reo!", NativeName = "Te Reo!", Description = "NZ Māori!", IconUrl = "/assets/core/deployed/images/flags/mi.svg", IsActive = true, IsDefault = false },
                new() { Code = "zh", Name = "Chinese!", NativeName = "中国人!", Description = "Chinese language!", IconUrl = "/assets/core/deployed/images/flags/china.svg", IsActive = true, IsDefault = false },
                new() { Code = "ws", Name = "Samoan!", NativeName = "Gagana Samoa!", Description = "Samoan language!", IconUrl = "/assets/core/deployed/images/flags/ws.svg", IsActive = true, IsDefault = false },
                new() { Code = "to", Name = "Tongan!", NativeName = "Tonga!", Description = "Tongan!", IconUrl = "/assets/core/deployed/images/flags/to.svg", IsActive = true, IsDefault = false },
                new() { Code = "in", Name = "Indian!", NativeName = "भारतीय!", Description = "Indian!", IconUrl = "/assets/core/deployed/images/flags/in.svg", IsActive = true, IsDefault = false }
            },
            
            GeneratedAt = DateTime.UtcNow
        };

        return context;
    }
}
