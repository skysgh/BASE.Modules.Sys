using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace App
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Extension methods for API documentation configuration.
    /// Supports Swagger UI at /documentation/apis/swagger
    /// </summary>
    public static class ApiDocumentationExtensions
    {
        /// <summary>
        /// Base path for all API documentation endpoints.
        /// </summary>
        private const string DocumentationBasePath = "/documentation/apis";

        /// <summary>
        /// Add API documentation services (Swagger).
        /// Call this during service configuration (before builder.Build()).
        /// </summary>
        public static IServiceCollection AddApiDocumentation(
            this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "BASE System API",
                    Description = "Multi-tenant system management API"
                });
            });
            
            return services;
        }

        /// <summary>
        /// Configure API documentation middleware.
        /// Call this after app.Build() in the middleware pipeline.
        /// </summary>
        public static WebApplication UseApiDocumentation(
            this WebApplication app)
        {
            // Swagger JSON at /documentation/apis/swagger/v1/swagger.json
            app.UseSwagger(c =>
            {
                c.RouteTemplate = $"{DocumentationBasePath}/swagger/{{documentName}}/swagger.json";
            });
            
            // Swagger UI at /documentation/apis/swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    $"{DocumentationBasePath}/swagger/v1/swagger.json",
                    "BASE System API v1");
                c.RoutePrefix = $"{DocumentationBasePath.TrimStart('/')}/swagger";
            });
            
            return app;
        }
    }
}


