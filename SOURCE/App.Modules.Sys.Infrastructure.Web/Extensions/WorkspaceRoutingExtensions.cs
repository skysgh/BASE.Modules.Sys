using App.Modules.Sys.Infrastructure.Services.Contracts;
using App.Modules.Sys.Infrastructure.Services.Implementations;
using App.Modules.Sys.Infrastructure.Web.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace App
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Extension methods for workspace routing.
    /// Simplifies setup to one-line calls in Program.cs.
    /// </summary>
    public static class WorkspaceRoutingExtensions
    {
        /// <summary>
        /// Add workspace routing services.
        /// Uses YOUR IAppConfiguration abstraction.
        /// </summary>
        public static IServiceCollection AddWorkspaceRouting(
            this IServiceCollection services,
            Action<WorkspaceRoutingOptions>? configure = null)
        {
            // Configure options (still using Microsoft.Extensions.Options internally in Infrastructure)
            if (configure != null)
            {
                services.Configure(configure);
            }
            else
            {
                services.Configure<WorkspaceRoutingOptions>(options =>
                {
                    options.DefaultWorkspace = "default";
                    options.AllowSubdomainWorkspaces = true;
                    options.AllowPathWorkspaces = true;
                });
            }

            // Register YOUR wrapper (isolates IOptions to Infrastructure)
            services.AddSingleton(typeof(IAppConfiguration<>), typeof(AppConfiguration<>));

            return services;
        }

        /// <summary>
        /// Use workspace routing middleware.
        /// Call this EARLY in the pipeline (before UseRouting).
        /// </summary>
        /// <param name="app">Application builder</param>
        public static IApplicationBuilder UseWorkspaceRouting(this IApplicationBuilder app)
        {
            return app.UseMiddleware<WorkspaceResolutionMiddleware>();
        }

        /// <summary>
        /// Configure workspace-aware endpoint routing.
        /// Handles optional workspace prefix in routes.
        /// </summary>
        /// <param name="builder">Route builder</param>
        public static void MapWorkspaceAwareRoutes(this IEndpointRouteBuilder builder)
        {
            // Route 1: With workspace prefix (path-based)
            // Pattern: /{workspaceId}/api/{apiType}/v{version:apiVersion}/{controller}/{action?}
            builder.MapControllerRoute(
                name: "workspace-api",
                pattern: "{workspaceId}/api/{apiType}/v{version:apiVersion}/{controller=Home}/{action=Index}/{id?}",
                defaults: null,
                constraints: new { workspaceId = new WorkspaceRouteConstraint() });

            // Route 2: Without workspace prefix (subdomain-based or default)
            // Pattern: /api/{apiType}/v{version:apiVersion}/{controller}/{action?}
            builder.MapControllerRoute(
                name: "default-api",
                pattern: "api/{apiType}/v{version:apiVersion}/{controller=Home}/{action=Index}/{id?}");

            // Route 3: Root (for health checks, static pages, etc.)
            builder.MapControllerRoute(
                name: "root",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
