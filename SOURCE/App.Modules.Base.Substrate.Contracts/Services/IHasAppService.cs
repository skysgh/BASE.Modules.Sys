using App.Modules.Base.Substrate.Contracts.Lifecycles;

namespace App.Modules.Base.Substrate.Services
{
    /// <summary>
    /// Contract for all Application Services, no matter what module
    /// they are in.
    /// <para>
    /// The contract does not add or enforce any functionality
    /// bar specifying a specific IoC Lifecycle (making it a Singleton
    /// by inheriting from <see cref="IHasSingletonLifecycle"/>).
    /// </para>
    /// <para>
    /// Service is specialised later per module. For example, in the 
    /// Core Module's Infrastructure assembly there is a 
    /// <c>IHasAppCoreService</c>, which all Services within that 
    /// Module implement.
    /// </para>
    /// </summary>
    /// <seealso cref="IHasSingletonLifecycle" />
    public interface IHasAppService : IHasSingletonLifecycle
    {
        // It is important to understand that 'Shared' 
        // will *not* have any Service *implementations*.
        // The contract is only here so that Services can be implemented 
        // in both Infrastructure, and Domain, without either knowing (ie
        // havin a Reference) between the two.

        // This assmebly is to remain light and dumb, basically only for 
        // Models, Messages, and tools to factory them
        // without referencing external services.

    }
}