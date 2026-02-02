using App.Modules.Sys.Domain.Configuration;
using App.Modules.Sys.Initialisation.Implementation.Base;
using App.Modules.Sys.Infrastructure.Services.Caching;
using App.Modules.Sys.Infrastructure.Services.Caching.Implementations;
using App.Modules.Sys.Infrastructure.Services.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Sys.Infrastructure.Initialisation
{
    /// <summary>
    /// Infrastructure module initializer.
    /// Registers core infrastructure services: caching, settings, etc.
    /// </summary>
    public class InfrastructureModuleAssemblyInitialiser : ModuleAssemblyInitialiserBase
    {
        /// <inheritdoc/>
        public override void DoBeforeBuild()
        {
            // ========================================
            // TIER 1: CACHE REGISTRY (In-Memory with Refresh)
            // ========================================
            Services.AddSingleton<ICacheRegistry, CacheRegistryService>();
            
            // ========================================
            // TIER 2: MEMORY CACHE (Backing Store)
            // ========================================
            Services.AddMemoryCache();
            
            // TODO: Add distributed cache when needed
            // Services.AddStackExchangeRedisCache(options => {
            //     options.Configuration = Configuration["Redis:ConnectionString"];
            // });
            
            // ========================================
            // SETTINGS SERVICE
            // ========================================
            Services.AddSingleton<ISettingsService, SettingsService>();
            
            // TODO: Implement and register repository
            // Services.AddScoped<ISettingsRepository, SettingsRepositoryEFCore>();
        }
        
        /// <inheritdoc/>
        public override void DoAfterBuild()
        {
            // Phase 2: Services are now available via DI
            // Can use ServiceProvider to get services and configure them
            
            // Example: Get cache registry and pre-warm commonly used caches
            // var cache = ServiceProvider.GetRequiredService<ICacheRegistry>();
            
            // Example: Register default system settings
            // var settings = ServiceProvider.GetRequiredService<ISettingsService>();
            // await settings.SetValueAsync("System/Appearance/Theme", "Light", modifiedBy: "SYSTEM");
        }
    }
}
