using System.Reflection;
using App.Modules.Base.Infrastructure.Data.EF.Schema.Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Modules.Base.Infrastructure.Data.EF.Schema.Implementations
{
    /// <summary>
    /// Orchestrates EF Core model building by discovering and invoking all entity configuration initializers.
    /// Works in BOTH runtime (DI) and design-time (migrations) scenarios.
    /// </summary>
    /// <remarks>
    /// <b>? Replaces ServiceLocator Anti-Pattern</b><br/>
    /// Uses proper DI at runtime + assembly scanning fallback for migrations
    /// <para>
    /// <b>?? Dual-Mode Operation:</b><br/>
    /// - Runtime: DI container provides initializers (fast, testable)<br/>
    /// - Design-Time: Assembly scanning discovers initializers (migrations work!)
    /// </para>
    /// </remarks>
    public class ModelBuilderOrchestrator : IModelBuilderOrchestrator
    {
        private readonly IServiceProvider? _serviceProvider;

        public ModelBuilderOrchestrator(IServiceProvider? serviceProvider = null)
        {
            _serviceProvider = serviceProvider;
        }

        public void Initialize(ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder));

            // Try DI first (runtime), fallback to scanning (migrations)
            var initializers = TryGetInitializersFromDI() 
                ?? DiscoverInitializersByAssemblyScanning(assemblies);

            ApplyInitializers(modelBuilder, initializers, assemblies);
        }

        private IEnumerable<IHasAppModuleDbContextModelBuilderInitializer>? TryGetInitializersFromDI()
        {
            if (_serviceProvider == null) return null;
            try { return _serviceProvider.GetServices<IHasAppModuleDbContextModelBuilderInitializer>(); }
            catch { return null; }
        }

        private IEnumerable<IHasAppModuleDbContextModelBuilderInitializer> DiscoverInitializersByAssemblyScanning(
            Assembly[] assemblies)
        {
            var assembliesToScan = assemblies.Any() ? assemblies : GetModuleAssemblies();

            var initializerTypes = assembliesToScan
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsInterface && !t.IsAbstract && 
                           typeof(IHasAppModuleDbContextModelBuilderInitializer).IsAssignableFrom(t));

            var initializers = new List<IHasAppModuleDbContextModelBuilderInitializer>();
            foreach (var type in initializerTypes)
            {
                try
                {
                    var instance = (IHasAppModuleDbContextModelBuilderInitializer)Activator.CreateInstance(type)!;
                    if (instance != null) initializers.Add(instance);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"??  Could not instantiate {type.Name}: {ex.Message}");
                }
            }
            return initializers;
        }

        private IEnumerable<Assembly> GetModuleAssemblies() =>
            AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name?.StartsWith("App.Modules.", StringComparison.OrdinalIgnoreCase) == true);

        private void ApplyInitializers(
            ModelBuilder modelBuilder,
            IEnumerable<IHasAppModuleDbContextModelBuilderInitializer> initializers,
            Assembly[] assemblies)
        {
            foreach (var initializer in initializers)
            {
                var type = initializer.GetType();

                if (typeof(IHasIgnoreThis).IsAssignableFrom(type)) continue;
                if (assemblies.Length > 0 && !assemblies.Contains(type.Assembly)) continue;

                try { initializer.Define(modelBuilder); }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(
                        $"? Failed applying {type.Name}. Check entity configurations.", ex);
                }
            }
        }
    }
}