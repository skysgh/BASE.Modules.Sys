using App.Modules.Sys.Infrastructure.AutoMapper.Services;
using App.Modules.Sys.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace App
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Extension methods for registering AutoMapper-based object mapping.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add AutoMapper implementation of IObjectMappingService.
        /// Scans Application assemblies for ObjectMapBase implementations.
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="assembliesToScan">Assemblies to scan for mappers (defaults to Application layer)</param>
        /// <returns>Service collection for chaining</returns>
        /// <remarks>
        /// Plugin architecture: This can be swapped with AddManualObjectMapping()
        /// or AddMapsterObjectMapping() without changing any other code.
        /// </remarks>
        public static IServiceCollection AddAutoMapperObjectMapping(
            this IServiceCollection services,
            params Assembly[] assembliesToScan)
        {
            // Default to scanning Application assembly if none specified
            if (assembliesToScan.Length == 0)
            {
                // Scan for Application assemblies by convention
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => a.GetName().Name?.Contains("Application", StringComparison.OrdinalIgnoreCase) == true)
                    .ToArray();

                assembliesToScan = assemblies;
            }

            // Register as singleton - mapper is stateless
            services.AddSingleton<IObjectMappingService>(
                sp => new AutoMapperObjectMappingService(assembliesToScan));

            return services;
        }

        /// <summary>
        /// Add AutoMapper with explicit assembly list.
        /// </summary>
        public static IServiceCollection AddAutoMapperObjectMapping(
            this IServiceCollection services,
            Type typeFromAssembly)
        {
            return services.AddAutoMapperObjectMapping(typeFromAssembly.Assembly);
        }
    }
}
