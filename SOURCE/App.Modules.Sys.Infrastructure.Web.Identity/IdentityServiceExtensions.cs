using App.Modules.Sys.Infrastructure.Web.Identity.Configuration;
using App.Modules.Sys.Infrastructure.Web.Identity.Providers;
using App.Modules.Sys.Infrastructure.Web.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Sys.Infrastructure.Web.Identity;

/// <summary>
/// Extension methods for registering identity services.
/// </summary>
public static class IdentityServiceExtensions
{
    /// <summary>
    /// Add BASE identity services with configured providers.
    /// 
    /// Supports multiple identity providers:
    /// - Azure AD (Microsoft Entra ID) - Enterprise SSO, FedRAMP High
    /// - Azure AD B2C - Consumer identity with local accounts, FedRAMP High
    /// - Local Accounts - Offline fallback with PBKDF2 password hashing
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Configuration.</param>
    /// <returns>Service collection for chaining.</returns>
    public static IServiceCollection AddBaseIdentity(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Bind options from configuration
        var identityOptions = new IdentityOptions();
        configuration.GetSection(IdentityOptions.SectionName).Bind(identityOptions);

        return services.AddBaseIdentity(configuration, identityOptions);
    }

    /// <summary>
    /// Add BASE identity services with explicit options.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Configuration.</param>
    /// <param name="options">Identity options.</param>
    /// <returns>Service collection for chaining.</returns>
    public static IServiceCollection AddBaseIdentity(
        this IServiceCollection services,
        IConfiguration configuration,
        IdentityOptions options)
    {
        // Register options
        services.Configure<IdentityOptions>(o =>
        {
            o.UseAzureAd = options.UseAzureAd;
            o.UseAzureAdB2C = options.UseAzureAdB2C;
            o.EnableLocalAccounts = options.EnableLocalAccounts;
            o.DefaultScheme = options.DefaultScheme;
            o.AzureAd = options.AzureAd;
            o.AzureAdB2C = options.AzureAdB2C;
            o.LocalAccounts = options.LocalAccounts;
        });

        // Determine default scheme
        var defaultScheme = options.UseAzureAd 
            ? JwtBearerDefaults.AuthenticationScheme 
            : options.UseAzureAdB2C 
                ? AzureAdB2CProvider.SchemeName 
                : JwtBearerDefaults.AuthenticationScheme;

        // Add authentication
        var authBuilder = services.AddAuthentication(authOptions =>
        {
            authOptions.DefaultScheme = defaultScheme;
            authOptions.DefaultChallengeScheme = defaultScheme;
        });

        // Add Azure AD if enabled
        if (options.UseAzureAd)
        {
            authBuilder.AddAzureAdAuthentication(configuration);
        }

        // Add Azure AD B2C if enabled
        if (options.UseAzureAdB2C)
        {
            authBuilder.AddAzureAdB2CAuthentication(configuration);
        }

        // Add authorization
        services.AddAuthorization();

        // Add local account services if enabled
        if (options.EnableLocalAccounts)
        {
            services.AddScoped<IPasswordHasher, Pbkdf2PasswordHasher>();
        }

        return services;
    }

    /// <summary>
    /// Add BASE identity services with configuration action.
    /// </summary>
    /// <param name="services">Service collection.</param>
    /// <param name="configuration">Configuration.</param>
    /// <param name="configureOptions">Options configuration action.</param>
    /// <returns>Service collection for chaining.</returns>
    public static IServiceCollection AddBaseIdentity(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<IdentityOptions> configureOptions)
    {
        var options = new IdentityOptions();
        configuration.GetSection(IdentityOptions.SectionName).Bind(options);
        configureOptions(options);

        return services.AddBaseIdentity(configuration, options);
    }
}
