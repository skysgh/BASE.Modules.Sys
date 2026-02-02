namespace App.Modules.Sys.Substrate.Contracts.Models
{
    /// <summary>
    /// Interface for types that can configure services after DI container is built.
    /// Phase 2 configuration - invoked after service provider is available.
    /// </summary>
    public interface IServiceConfigurer
    {
        /// <summary>
        /// Name of the service being configured (for logging/debugging).
        /// </summary>
        string ServiceName { get; }

        /// <summary>
        /// Configure services that require access to IServiceProvider.
        /// </summary>
        /// <param name="serviceProvider">The built service provider.</param>
        void Configure(IServiceProvider serviceProvider);
    }
}