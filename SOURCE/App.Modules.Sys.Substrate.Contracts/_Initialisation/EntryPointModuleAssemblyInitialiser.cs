
using System;
using System.Linq;
using System.Reflection;
using App.Modules.Sys.Shared.Models.Enums;
using App.Modules.Sys.Shared.Models.Implementations;
using App.Modules.Sys.Substrate.Contracts.Models;

namespace App.Modules.Sys.Substrate.Contracts.Initialisation
{
    /// <summary>
    /// Entry point module initializer - discovers all modules and initializes them.
    /// All logic delegated to extension methods for maximum reusability.
    /// </summary>
    public class EntryPointModuleAssemblyInitialiser
    {
        /// <summary>
        /// ONE METHOD - discovers and initializes everything recursively.
        /// </summary>
        /// <param name="entryPointAssembly">Entry assembly (App.Host)</param>
        /// <param name="configuration">Configuration object (passed as object to minimize dependencies)</param>
        /// <param name="log">Startup log</param>
        public static ModuleConfigurationBag Initialize(
            Assembly entryPointAssembly,
            object configuration,
            StartupLog log)
        {
            log.Log(TraceLevel.Info, "=== DISCOVERING MODULES ===");
            
            // STEP 1: Discover all module assemblies recursively (extension method)
            var moduleAssemblies = entryPointAssembly.DiscoverModuleAssemblies();
            
            // STEP 2: Sort in dependency order using extension method
            var sortedAssemblies = moduleAssemblies.ToList().TopologicalSort();
            
            log.Log(TraceLevel.Info, $"Found {sortedAssemblies.Count} module assemblies in dependency order:");
            for (int i = 0; i < sortedAssemblies.Count; i++)
            {
                log.Log(TraceLevel.Info, $"  [{i}] {sortedAssemblies[i].GetName().Name}");
            }
            
            // STEP 3: Process each assembly using extension methods
            log.Log(TraceLevel.Info, "=== PROCESSING ASSEMBLIES ===");
            var bag = new ModuleConfigurationBag { ModuleName = "Application" };
            
            foreach (var assembly in sortedAssemblies)
            {
                ProcessAssembly(assembly, bag, log);
            }
            
            log.Log(TraceLevel.Info, 
                $"=== INITIALIZATION COMPLETE ===");
            log.Log(TraceLevel.Info,
                $"Services: {bag.LocalServices.Count}, " +
                $"Mappers: {bag.MapperProfiles.Count}, " +
                $"Schemas: {bag.DbSchemaTypes.Count}, " +
                $"Configurers: {bag.ServiceConfigurers.Count}");
            
            return bag;
        }
        
        /// <summary>
        /// Process one assembly - all logic delegated to extension methods.
        /// This method is just orchestration.
        /// </summary>
        private static void ProcessAssembly(
            Assembly assembly,
            ModuleConfigurationBag bag,
            StartupLog log)
        {
            var assemblyName = assembly.GetName().Name;
            log.Log(TraceLevel.Info, $"Processing: {assemblyName}");
            
            // Discover services (extension method)
            var services = assembly.DiscoverServices(log);
            bag.LocalServices.AddRange(services);
            
            // Discover mapper profiles (extension method)
            var mappers = assembly.DiscoverMapperProfiles(log);
            bag.MapperProfiles.AddRange(mappers);
            
            // Discover DB schemas (extension method)
            var schemas = assembly.DiscoverDbSchemaTypes(log);
            bag.DbSchemaTypes.AddRange(schemas);
            
            // Discover service configurers (extension method)
            var configurers = assembly.DiscoverServiceConfigurers(log);
            bag.ServiceConfigurers.AddRange(configurers.Cast<IServiceConfigurer>());
            
            // Log summary for this assembly
            if (services.Count > 0 || mappers.Count > 0 || schemas.Count > 0 || configurers.Count > 0)
            {
                log.Log(TraceLevel.Info, 
                    $"  ? Services: {services.Count}, " +
                    $"Mappers: {mappers.Count}, " +
                    $"Schemas: {schemas.Count}, " +
                    $"Configurers: {configurers.Count}");
            }
            else
            {
                log.Log(TraceLevel.Debug, $"  ? (empty assembly)");
            }
        }
    }
}
