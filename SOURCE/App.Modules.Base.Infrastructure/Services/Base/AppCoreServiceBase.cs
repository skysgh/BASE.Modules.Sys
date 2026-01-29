// using App.Modules.Base.Infrastructure.NewFolder.Services;

namespace App.Modules.Base.Infrastructure.NewFolder.Services.Base
{

    /// <summary>
    /// Abstract base classs for all Core Infrastructure Services.
    /// <para>
    /// Adds no functionality - beyond ensuring
    /// clases implement the <see cref="IHasAppBaseInfrastructureService"/>
    /// interface.
    /// </para>
    /// <para>
    /// TODO: If it's not adding any functionality, 
    /// not sure why we need it. Just inherit from the contract,no?
    /// </para>
    /// </summary>
    /// <seealso cref="IHasAppBaseInfrastructureService" />
    public abstract class AppCoreServiceBase : IHasAppBaseInfrastructureService
    {
    }
}