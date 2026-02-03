using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using App.Modules.Sys.Infrastructure.Diagnostics;
using App.Modules.Sys.Shared.Services.Caching;

namespace App.Modules.Sys.Infrastructure.Caching.Examples
{
    /// <summary>
    /// Example cache object: Application configuration settings.
    /// This demonstrates how to create a self-contained cache object
    /// that will be automatically discovered and registered at startup.
    /// Now with dependency injection support!
    /// </summary>
    public class AppConfigCacheObject : CacheObjectBase<Dictionary<string, string>>
    {
        private readonly IAppLogger<AppConfigCacheObject>? _logger;

        /// <summary>
        /// Constructor with optional DI dependencies.
        /// ActivatorUtilities will inject these automatically.
        /// </summary>
        public AppConfigCacheObject(IAppLogger<AppConfigCacheObject>? logger = null)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public override string Key => "System.Configuration.AppSettings";

        /// <inheritdoc/>
        public override TimeSpan? Duration => TimeSpan.FromMinutes(15);

        /// <summary>
        /// Load application configuration settings.
        /// This is called automatically when the cache expires.
        /// </summary>
        protected override async Task<Dictionary<string, string>> GetValueAsync(CancellationToken ct = default)
        {
            _logger?.LogInformation("Loading app configuration from source...");
            
            // Simulate loading from database, file, or API
            await Task.Delay(100, ct); // Simulate I/O

            // In real implementation, this would load from:
            // - Database
            // - Configuration file
            // - Azure Key Vault
            // - Environment variables
            // etc.

            _logger?.LogInformation("App configuration loaded successfully");

            return new Dictionary<string, string>
            {
                ["Environment"] = "Development",
                ["LogLevel"] = "Information",
                ["EnableFeatureX"] = "true"
            };
        }
    }

    /// <summary>
    /// Example: Cached list of supported languages.
    /// Never expires (static data).
    /// </summary>
    public class SupportedLanguagesCacheObject : CacheObjectBase<List<string>>
    {
        /// <inheritdoc/>
        public override string Key => "System.SupportedLanguages";

        /// <inheritdoc/>
        public override TimeSpan? Duration => null; // Never expires

        /// <inheritdoc/>
        protected override Task<List<string>> GetValueAsync(CancellationToken ct = default)
        {
            // Static data - never changes
            var languages = new List<string>
            {
                "en-US",
                "en-GB",
                "fr-FR",
                "de-DE",
                "es-ES"
            };

            return Task.FromResult(languages);
        }
    }
}

