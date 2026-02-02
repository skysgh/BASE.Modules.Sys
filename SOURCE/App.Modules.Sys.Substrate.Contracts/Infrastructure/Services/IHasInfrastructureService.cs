using App.Modules.Sys.Infrastructure.Lifecycles;
using App.Modules.Sys.Shared.Services;

namespace App.Modules.Sys.Infrastructure.Services
{
    /// <summary>
    /// Contract for all System Infrastructure Services.
    /// <para>
    /// The contract does not add or enforce any functionality
    /// bar specifying a specific IoC Lifecycle (making it a Singleton
    /// by inheriting from <see cref="IHasSingletonLifecycle"/>)
    /// and allowing for filtering per Core/Module.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Implements <see cref="IHasService"/>,
    /// in order for the Dependency 
    /// Injection service to easily find it at startup.
    /// </para>
    /// </remarks>
    /// <seealso cref="IHasService" />
    public interface IHasInfrastructureService : IHasService
    {

    }
}