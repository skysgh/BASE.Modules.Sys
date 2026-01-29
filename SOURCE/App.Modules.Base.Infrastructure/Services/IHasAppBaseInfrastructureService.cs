using App.Modules.Base.Substrate.Contracts.Lifecycles;
using App.Modules.Base.Substrate.Services;

namespace App.Modules.Base.Infrastructure.NewFolder.Services
{
    /// <summary>
    /// Contract for all System Base Infrastructure Services.
    /// <para>
    /// The contract does not add or enforce any functionality
    /// bar specifying a specific IoC Lifecycle (making it a Singleton
    /// by inheriting from <see cref="IHasSingletonLifecycle"/>)
    /// and allowing for filtering per Core/Module.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Implements <see cref="IHasAppService"/>,
    /// in order for the Dependency 
    /// Injection service to easily find it at startup.
    /// </para>
    /// </remarks>
    /// <seealso cref="IHasAppService" />
    public interface IHasAppBaseInfrastructureService : IHasAppInfrastructureService
    {
    }
}