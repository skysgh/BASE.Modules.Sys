using App.Modules.Sys.Infrastructure.Data.EF.Schema.Management;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace App.Modules.Sys.Infrastructure.Data.EF.Schema.Implementations
{
    /// <summary>
    /// DESIGN-TIME orchestrator for EF Core migrations.
    /// Uses reflection to scan assemblies - NO DI available in PowerShell/CLI!
    /// </summary>
    internal sealed class DesignTimeModelBuilderOrchestrator : IModelBuilderOrchestrator
    {
        public void Initialize(ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);

            // Iterate each assembly to find entity configurations
            foreach (var assembly in assemblies)
            {
                // REFLECTION SCAN: Find all IEntityTypeConfiguration<T> implementations
                // WHY? At design-time we have NO DI, NO ModuleConfigurationBag
                // We MUST scan assemblies to discover what exists
                var types = assembly.GetTypes()
                    .Where(t => t.IsClass &&                    // Concrete class only
                               !t.IsAbstract &&                 // Must be instantiable
                               t.GetInterfaces()                // Check interfaces
                                   .Any(i => i.IsGenericType && // Generic interface
                                        i.Name.Contains("IEntityTypeConfiguration"))); // EF config marker

                // Apply each discovered configuration
                foreach (var schemaType in types.OrderBy(t => t.Name))
                {
                    // STEP 1: Create instance (requires parameterless constructor!)
                    var instance = Activator.CreateInstance(schemaType)!;

                    // STEP 2: Extract TEntity from IEntityTypeConfiguration<TEntity>
                    // Example: UserConfig implements IEntityTypeConfiguration<User>
                    // We need to extract "User" type
                    var entityType = schemaType.GetInterfaces()
                        .First(i => i.IsGenericType && i.Name.Contains("IEntityTypeConfiguration"))
                        .GetGenericArguments()[0];  // Gets the TEntity

                    // STEP 3: Dynamically invoke modelBuilder.ApplyConfiguration<TEntity>(instance)
                    // Can't use compile-time generics - entity type is discovered at runtime!
                    // Equivalent to: modelBuilder.ApplyConfiguration<User>(userConfig)
                    typeof(ModelBuilder)
                        .GetMethod("ApplyConfiguration")!
                        .MakeGenericMethod(entityType)
                        .Invoke(modelBuilder, new[] { instance });
                }
            }
        }
    }
}