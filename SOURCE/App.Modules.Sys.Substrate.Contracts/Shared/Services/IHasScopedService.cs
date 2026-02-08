using App.Modules.Sys.Shared.Lifecycles;

namespace App.Modules.Sys.Shared.Services;

/// <summary>
/// Contract that scoped Infrastructure and Domain 
/// services must implement to be discoverable
/// at startup via reflection.
/// </summary>
/// <remarks>
/// Similar to <see cref="IHasService"/> but for scoped (per-request) services.
/// Use this when the service needs per-request lifetime.
/// </remarks>
public interface IHasScopedService : IHasScopedLifecycle
{
}
