using App.Modules.Sys.Application.Domains.Context.Models.Implementations;
using App.Modules.Sys.Application.ReferenceData.Models;
using App.Modules.Sys.Shared.Services;

namespace App.Modules.Sys.Application.Domains.Context.Services.Implementations;

/// <summary>
/// Stub implementation of system context provider.
/// TODO: Replace with database-backed implementation.
/// </summary>
public class StubSystemContextProvider : ISystemContextProvider, IHasService
{
    /// <inheritdoc/>
    public Task<SystemContextDto> GetSystemContextAsync(CancellationToken ct = default)
    {
        var context = new SystemContextDto
        {
            Name = "BASE Platform",
            Version = "1.0.0",
            Environment = GetEnvironment(),
            Branding = new SystemBrandingDto
            {
                Provider = new ProviderInfoDto
                {
                    Name = "BASE Software Inc",
                    ContactEmail = "support@base-software.com",
                    LogoUrl = "/assets/system/logo.png",
                    SupportUrl = "https://support.base-software.com",
                    LegalUrl = "https://base-software.com/legal"
                },
                Developer = new DeveloperInfoDto
                {
                    Name = "BASE Development Team",
                    ContactEmail = "dev@base-software.com",
                    DocsUrl = "https://docs.base-software.com"
                }
            },
            Features = new Dictionary<string, bool>
            {
                { "enableMultiTenant", true },
                { "enableAdvancedSettings", true },
                { "enableDiagnostics", true }
            }
        };

        return Task.FromResult(context);
    }

    private static string GetEnvironment()
    {
        return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
    }
}
