using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace App.Modules.Sys.Infrastructure.Web.Identity.Providers;

/// <summary>
/// Configures Azure AD (Microsoft Entra ID) as an identity provider.
/// 
/// This is the enterprise-grade, FedRAMP-authorized identity provider
/// for organizational SSO scenarios.
/// 
/// Use for:
/// - Enterprise customers with their own Azure AD
/// - Government agencies (Azure Government)
/// - Organizations requiring SSO with Microsoft 365
/// </summary>
public static class AzureAdProvider
{
    /// <summary>
    /// Authentication scheme name for Azure AD.
    /// </summary>
    public const string SchemeName = "AzureAd";

    /// <summary>
    /// Add Azure AD authentication.
    /// </summary>
    /// <param name="builder">Authentication builder.</param>
    /// <param name="configuration">Configuration root.</param>
    /// <returns>The authentication builder for chaining.</returns>
    public static AuthenticationBuilder AddAzureAdAuthentication(
        this AuthenticationBuilder builder,
        IConfiguration configuration)
    {
        // Use Microsoft.Identity.Web which is the official, supported library
        builder.AddMicrosoftIdentityWebApi(
            configuration.GetSection("AzureAd"),
            jwtBearerScheme: JwtBearerDefaults.AuthenticationScheme,
            subscribeToJwtBearerMiddlewareDiagnosticsEvents: true);

        return builder;
    }

    /// <summary>
    /// Add Azure AD authentication with custom options.
    /// </summary>
    /// <param name="builder">Authentication builder.</param>
    /// <param name="options">Azure AD options.</param>
    /// <returns>The authentication builder for chaining.</returns>
    public static AuthenticationBuilder AddAzureAdAuthentication(
        this AuthenticationBuilder builder,
        Configuration.AzureAdOptions options)
    {
        builder.AddMicrosoftIdentityWebApi(
            jwtOptions =>
            {
                jwtOptions.Audience = options.ClientId;
            },
            identityOptions =>
            {
                identityOptions.Instance = options.Instance;
                identityOptions.TenantId = options.TenantId;
                identityOptions.ClientId = options.ClientId;
                identityOptions.ClientSecret = options.ClientSecret;
            },
            jwtBearerScheme: JwtBearerDefaults.AuthenticationScheme,
            subscribeToJwtBearerMiddlewareDiagnosticsEvents: true);

        return builder;
    }
}
