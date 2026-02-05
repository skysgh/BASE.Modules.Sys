using App.Modules.Sys.Application.Domains.Context.Models.Implementations;
using App.Modules.Sys.Shared.Lifecycles;

namespace App.Modules.Sys.Application.Domains.Context;

/// <summary>
/// Service for aggregating and building application context.
/// This is the orchestrator that combines system, workspace, user, and computed settings.
/// Different from IContextService (per-request storage) in Infrastructure - this builds APPLICATION context.
/// Auto-registered via IHasScopedLifecycle.
/// </summary>
public interface IApplicationContextService : IHasScopedLifecycle
{
    /// <summary>
    /// Gets the complete application context for the current request.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Complete application context DTO.</returns>
    Task<ApplicationContextDto> GetApplicationContextAsync(CancellationToken cancellationToken = default);
}

