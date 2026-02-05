using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Schema;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Schema.Implementations
{
    /// <summary>
    /// Orchestrates EF Core model building by applying schema types.
    /// Uses types discovered during module initialization (from ModuleConfigurationBag).
    /// </summary>
    public class RunTimeModelBuilderOrchestrator : IModelBuilderOrchestrator
    {
        private readonly IEnumerable<Type> _schemaTypes;

        /// <summary>
        /// Constructor - receives schema types from ModuleConfigurationBag.
        /// </summary>
        public RunTimeModelBuilderOrchestrator(IEnumerable<Type> schemaTypes)
        {
            _schemaTypes = schemaTypes ?? Enumerable.Empty<Type>();
        }

        /// <summary>
        /// Applies all discovered IEntityTypeConfiguration types to ModelBuilder.
        /// </summary>
        public void Initialize(ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            var typesToApply = assemblies.Length > 0
                ? _schemaTypes.Where(t => assemblies.Contains(t.Assembly))
                : _schemaTypes;

            foreach (var schemaType in typesToApply.OrderBy(t => t.Name))
            {
                ApplyConfiguration(modelBuilder, schemaType);
            }
        }

        private static void ApplyConfiguration(ModelBuilder modelBuilder, Type schemaType)
        {
            try
            {
                var instance = Activator.CreateInstance(schemaType)!;

                var entityType = schemaType.GetInterfaces()
                    .First(i => i.IsGenericType &&
                               i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    .GetGenericArguments()[0];

                var applyMethod = typeof(ModelBuilder)
                    .GetMethod(nameof(ModelBuilder.ApplyConfiguration))!
                    .MakeGenericMethod(entityType);

                applyMethod.Invoke(modelBuilder, new[] { instance });
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Failed applying schema {schemaType.Name}.", ex);
            }
        }
    }
}
