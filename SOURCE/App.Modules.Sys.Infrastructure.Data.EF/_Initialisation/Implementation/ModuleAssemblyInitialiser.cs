using App.Modules.Sys.Infrastructure.Data.EF.Interceptors;
using App.Modules.Sys.Infrastructure.Data.EF.Services;
using App.Modules.Sys.Initialisation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace App.Modules.Sys.Infrastructure.Data.EF.Initialisation.Implementation
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Assembly specific implementation of IModuleAssemblyInitialiser.
    /// Discovers and registers DbContext save handlers from this assembly.
    /// </summary>
    public class ModuleAssemblyInitialiser : IModuleAssemblyInitialiser
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ModuleAssemblyInitialiser(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public void DoBeforeBuild()
        {
        }

        /// <inheritdoc/>
        public void DoAfterBuild()
        {


            // Get the handler registry (singleton)
            var registry = _serviceProvider.GetService<IDbContextSaveHandlerRegistryService>();
            if (registry != null)
            {
                registry.DiscoverAndRegister(Assembly.GetExecutingAssembly());
            }
        }

    }
}