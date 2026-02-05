using App.Modules.Sys.Application.Domains.Workspace.CacheObjects;
using App.Modules.Sys.Application.Domains.Workspace.Services;
using App.Modules.Sys.Application.Domains.Workspace.Services.Implementations;
using App.Modules.Sys.Shared.Services.Caching;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace App
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Extension methods for workspace caching using YOUR ICacheObjectRegistry pattern.
    /// </summary>
    public static class WorkspaceCacheExtensions
    {
        /// <summary>
        /// Add workspace validation with YOUR cache object pattern.
        /// </summary>
        public static IServiceCollection AddWorkspaceValidation(
            this IServiceCollection services)
        {
            // Register workspace validation service
            services.AddSingleton<IWorkspaceValidationService, CachedWorkspaceValidationService>();
            
            // Register cache object for discovery at startup
            services.AddSingleton<ICacheObject, WorkspaceIdsCacheObject>();
            
            return services;
        }

        /// <summary>
        /// Preload workspace cache at startup.
        /// </summary>
        public static async Task PreloadWorkspaceCacheAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var cacheRegistry = scope.ServiceProvider.GetRequiredService<ICacheObjectRegistryService>();
            
            // Trigger load
            await cacheRegistry.GetValueAsync<System.Collections.Generic.HashSet<string>>("Workspace.Ids");
        }
    }

}
