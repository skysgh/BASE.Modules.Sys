using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Services;
using App.Modules.Sys.Initialisation;
using App.Modules.Sys.Initialisation.Implementation.Base;
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
    public class ModuleAssemblyInitialiser : ModuleAssemblyInitialiserBase
    {

        ///<inheritdoc/>
        public override void DoBeforeBuild(IServiceCollection services)
        {
        }

        /// <inheritdoc/>
        public override void DoAfterBuild(IServiceProvider serviceProvider)
        {

        // Get the handler registry (singleton)
        var registry = serviceProvider.GetService<IDbContextSaveHandlerRegistryService>();
            if (registry != null)
            {
                registry.DiscoverAndRegister(Assembly.GetExecutingAssembly());
            }
        }

    }
}