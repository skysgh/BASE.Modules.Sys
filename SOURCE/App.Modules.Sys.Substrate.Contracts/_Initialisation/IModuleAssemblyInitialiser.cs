using App.Modules.Sys.Shared.Lifecycles;
using App.Modules.Sys.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace App.Modules.Sys.Initialisation
{
    /// <summary>
    /// Contract for Initialisers of Attributes within a Logical Module.
    /// Dependencies are passed as parameters to enable proper initialization.
    /// </summary>
    public interface IModuleAssemblyInitialiser : IHasSingletonLifecycle
    {
        /// <summary>
        /// Do the following *before* the service provider is initialised.
        /// Use this to register services, AutoMapper profiles, etc.
        /// </summary>
        /// <param name="services">Service collection to register services into</param>
        void DoBeforeBuild(IServiceCollection services);
        
        /// <summary>
        /// Do this *after* the service provider is built.
        /// At this point, can resolve services from DI.
        /// Use this to configure registry services and similar.
        /// </summary>
        /// <param name="serviceProvider">Built service provider for resolving services</param>
        void DoAfterBuild(IServiceProvider serviceProvider);
    }
}

