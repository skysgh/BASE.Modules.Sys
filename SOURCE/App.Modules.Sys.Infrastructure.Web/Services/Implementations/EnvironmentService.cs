using App.Modules.Sys.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace App.Modules.Sys.Infrastructure.Web.Services.Implementations;

/// <summary>
/// Web-specific implementation of environment service.
/// Provides deployment environment information and configuration access.
/// </summary>
/// <remarks>
/// Uses IWebHostEnvironment for web-specific features (WebRootPath).
/// Singleton lifetime - environment doesn't change during runtime.
/// 
/// Located in Infrastructure.Web because it depends on ASP.NET Core types.
/// </remarks>
public sealed class EnvironmentService : IEnvironmentService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public EnvironmentService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
    }

    // ========================================
    // ENVIRONMENT IDENTIFICATION
    // ========================================

    public string EnvironmentName => _webHostEnvironment.EnvironmentName;

    public bool IsDevelopment => _webHostEnvironment.IsDevelopment();

    public bool IsStaging => _webHostEnvironment.IsStaging();

    public bool IsProduction => _webHostEnvironment.IsProduction();

    public string ApplicationName => _webHostEnvironment.ApplicationName;

    // ========================================
    // FILE SYSTEM PATHS
    // ========================================

    public string ContentRootPath => _webHostEnvironment.ContentRootPath;

    // ✅ REAL WebRootPath (not computed) - from IWebHostEnvironment
    public string WebRootPath => _webHostEnvironment.WebRootPath;

    public string GetAbsolutePath(string relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
        {
            throw new ArgumentException("Relative path cannot be null or whitespace", nameof(relativePath));
        }

        // Remove leading slash if present
        relativePath = relativePath.TrimStart('/', '\\');

        return Path.Combine(ContentRootPath, relativePath);
    }

    // ========================================
    // ENVIRONMENT CHECKS
    // ========================================

    public bool IsEnvironment(string environmentName)
    {
        if (string.IsNullOrWhiteSpace(environmentName))
        {
            throw new ArgumentException("Environment name cannot be null or whitespace", nameof(environmentName));
        }

        return _webHostEnvironment.IsEnvironment(environmentName);
    }

    // ========================================
    // FEATURE FLAGS
    // ========================================

    public bool IsFeatureEnabled(string featureName)
    {
        if (string.IsNullOrWhiteSpace(featureName))
        {
            throw new ArgumentException("Feature name cannot be null or whitespace", nameof(featureName));
        }

        // In development, most features are enabled by default
        if (IsDevelopment)
        {
            return true;
        }

        // In production, check environment variables or configuration
        var envVarName = $"FEATURE_{featureName.ToUpperInvariant().Replace('.', '_')}";
        var envValue = Environment.GetEnvironmentVariable(envVarName);

        return !string.IsNullOrEmpty(envValue) && 
               (envValue.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                envValue.Equals("1", StringComparison.OrdinalIgnoreCase) ||
                envValue.Equals("enabled", StringComparison.OrdinalIgnoreCase));
    }

    public bool ShouldExposeDetailedErrors()
    {
        // Only expose detailed errors in development
        return IsDevelopment;
    }

    public bool ShouldEnableSwagger()
    {
        // Swagger in development and staging, not in production
        return IsDevelopment || IsStaging;
    }

    // ========================================
    // CONFIGURATION
    // ========================================

    public string GetEnvironmentVariable(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or whitespace", nameof(key));
        }

        return Environment.GetEnvironmentVariable(key) ?? string.Empty;
    }

    public string GetEnvironmentVariableOrDefault(string key, string defaultValue)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Key cannot be null or whitespace", nameof(key));
        }

        var value = Environment.GetEnvironmentVariable(key);
        return string.IsNullOrEmpty(value) ? defaultValue : value;
    }
}
