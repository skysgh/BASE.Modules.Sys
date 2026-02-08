using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace App.Modules.Sys.Infrastructure.Web.Identity.Providers;

/// <summary>
/// Configures Azure AD B2C as an identity provider.
/// 
/// This is the consumer-facing, FedRAMP-authorized identity provider that supports:
/// - Local accounts (email + password managed by B2C)
/// - Social identity providers (Google, Facebook, Microsoft, etc.)
/// - Enterprise federation (SAML, OIDC)
/// - Custom policies for complex scenarios
/// 
/// Use for:
/// - Consumer-facing applications
/// - Applications needing local accounts without self-hosting
/// - Applications requiring social login
/// - Government applications needing FedRAMP compliance with local accounts
/// </summary>
public static class AzureAdB2CProvider
{
    /// <summary>
    /// Authentication scheme name for Azure AD B2C.
    /// </summary>
    public const string SchemeName = "AzureAdB2C";

    /// <summary>
    /// Add Azure AD B2C authentication.
    /// </summary>
    /// <param name="builder">Authentication builder.</param>
    /// <param name="configuration">Configuration root.</param>
    /// <returns>The authentication builder for chaining.</returns>
    public static AuthenticationBuilder AddAzureAdB2CAuthentication(
        this AuthenticationBuilder builder,
        IConfiguration configuration)
    {
        // Use Microsoft.Identity.Web for B2C
        builder.AddMicrosoftIdentityWebApi(
            configuration.GetSection("AzureAdB2C"),
            jwtBearerScheme: SchemeName,
            subscribeToJwtBearerMiddlewareDiagnosticsEvents: true);

        return builder;
    }

    /// <summary>
    /// Add Azure AD B2C authentication with custom options.
    /// </summary>
    /// <param name="builder">Authentication builder.</param>
    /// <param name="options">Azure AD B2C options.</param>
    /// <returns>The authentication builder for chaining.</returns>
    public static AuthenticationBuilder AddAzureAdB2CAuthentication(
        this AuthenticationBuilder builder,
        Configuration.AzureAdB2COptions options)
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
                identityOptions.Domain = options.Domain;
                identityOptions.SignUpSignInPolicyId = options.SignUpSignInPolicyId;
                identityOptions.ResetPasswordPolicyId = options.ResetPasswordPolicyId;
                identityOptions.EditProfilePolicyId = options.EditProfilePolicyId;
            },
            jwtBearerScheme: SchemeName,
            subscribeToJwtBearerMiddlewareDiagnosticsEvents: true);

        return builder;
    }
}
