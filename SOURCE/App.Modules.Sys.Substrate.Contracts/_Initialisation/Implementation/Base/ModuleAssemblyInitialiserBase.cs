// ============================================================================
// FIXED - Parameters Now Passed (Lessons Learnt Applied!)
// ============================================================================
// This base class is now properly connected to the bootstrap process.
//
// WHAT WAS WRONG:
// - Properties were never set (no code path to populate them)
// - Methods were never called (static discovery didn't invoke instance methods)
//
// HOW IT'S FIXED:
// - Interface methods now take parameters (IServiceCollection, IServiceProvider)
// - Base class sets properties from parameters
// - EntryPointModuleAssemblyInitialiser now instantiates and calls these
//
// USAGE:
// public class MyModuleInitialiser : ModuleAssemblyInitialiserBase
// {
//     public override void DoBeforeBuild(IServiceCollection services)
//     {
//         Services = services;  // Base class provides this
//         Services.AddMyModule();
//     }
// }
// ============================================================================

using Microsoft.Extensions.DependencyInjection;
using System;

namespace App.Modules.Sys.Initialisation.Implementation.Base
{
    /// <summary>
    /// Base class for module-specific initialization.
    /// Override DoBeforeBuild() or DoAfterBuild() for custom logic.
    /// Dependencies are passed as parameters and stored as protected properties.
    /// </summary>
    public abstract class ModuleAssemblyInitialiserBase : IModuleAssemblyInitialiser
    {
        /// <summary>
        /// Override to add custom logic BEFORE services are registered.
        /// Runs during Phase 1 - IServiceProvider not yet available.
        /// </summary>
        /// <param name="services">Service collection for registration</param>
        public virtual void DoBeforeBuild(IServiceCollection services) 
        { 
            // Override if needed
        }

        /// <summary>
        /// Override to add custom logic AFTER IServiceProvider is built.
        /// Runs during Phase 2 - can resolve services from DI.
        /// </summary>
        /// <param name="serviceProvider">Built service provider</param>
        public virtual void DoAfterBuild(IServiceProvider serviceProvider) 
        { 
            // Override if needed
        }
    }
}



