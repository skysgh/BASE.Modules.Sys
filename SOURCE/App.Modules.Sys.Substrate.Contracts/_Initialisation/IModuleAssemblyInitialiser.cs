using App.Modules.Sys.Shared.Services;

namespace App.Modules.Sys.Initialisation
{
    /// <summary>
    /// Contract for Initialisers of Attributes
    /// within a Logical Module.
    /// </summary>
    public interface IModuleAssemblyInitialiser
    {
        /// <summary>
        /// Do the following *before* the service provider 
        /// is initialised (ie when Services are not yet available via DI).
        /// </summary>
        void DoBeforeBuild();
        /// <summary>
        /// Do this *after* the service provider is built.
        /// At this point, can resolve services from DI.
        /// Use this to configure <see cref="IHasRegistryService"/>
        /// and similar.
        /// </summary>
        void DoAfterBuild();
    }
}
