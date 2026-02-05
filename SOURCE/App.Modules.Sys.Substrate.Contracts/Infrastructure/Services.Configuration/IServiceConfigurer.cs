using App.Modules.Sys.Shared.Lifecycles;

namespace App.Modules.Sys.Infrastructure.Services.Configuration
{
    /// <summary>
    /// Interface for types that can configure services after DI container is built.
    /// Phase 2 configuration - invoked after service provider is available.
    /// Implements IHasSingletonLifecycle for discovery and DI registration.
    /// </summary>
    public interface IServiceConfigurer : IHasSingletonLifecycle
    {
        /// <summary>
        /// Name of the service being configured (for logging/debugging).
        /// </summary>
        string ServiceName { get; }

        /// <summary>
        /// Order of execution (lower numbers run first).
        /// Use this to ensure dependencies are configured in correct order.
        /// Example: DatabaseConfigurer should have Order = 0 to run first.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Configure services that require access to IServiceProvider.
        /// Called after DI container is built, in Order sequence.
        /// </summary>
        /// <param name="serviceProvider">The built service provider.</param>
        void Configure(IServiceProvider serviceProvider);
    }
}
