using App.Modules.Sys.Initialisation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace App.Modules.Sys.Substrate.Contracts.Models
{
    /// <summary>
    /// Configuration bag collected during module initialization.
    /// Contains all discovered services, mappers, schemas, handlers, configurers, and initializers.
    /// </summary>
    public class ModuleConfigurationBag
    {
        /// <summary>
        /// Name of the module (e.g., "Base", "Core", "Application")
        /// </summary>
        public string ModuleName { get; init; } = string.Empty;

        /// <summary>
        /// Services discovered via reflection (lifecycle markers).
        /// </summary>
        public List<ServiceDescriptor> LocalServices { get; } = new();

        /// <summary>
        /// Remote service placeholders (configured in Phase 2).
        /// </summary>
        public List<ServiceDescriptor> RemoteServicePlaceholders { get; } = new();

        /// <summary>
        /// Service configurers that need IServiceProvider to activate (Phase 2).
        /// </summary>
        public List<IServiceConfigurer> ServiceConfigurers { get; } = new();

        /// <summary>
        /// Module initializers - called DoBeforeBuild in Phase 1, DoAfterBuild in Phase 2.
        /// </summary>
        public List<IModuleAssemblyInitialiser> ModuleInitializers { get; set; } = new();

        /// <summary>
        /// AutoMapper Profile types and descriptions.
        /// </summary>
        public List<(Type Profile, string Description)> MapperProfiles { get; } = new();

        /// <summary>
        /// EF Core schema configurations (IEntityTypeConfiguration).
        /// </summary>
        public List<Type> DbSchemaTypes { get; } = new();

        /// <summary>
        /// DbContext save handlers (pre/post save operations).
        /// </summary>
        public List<Type> DbContextHandlers { get; } = new();

        /// <summary>
        /// Cache objects discovered via reflection (ICacheObject implementers).
        /// </summary>
        public List<ServiceDescriptor> CacheObjects { get; } = new();

        /// <summary>
        /// Notes about the initialization process.
        /// </summary>
        public List<string> Notes { get; } = new();
    }
}