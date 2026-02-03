namespace App.Modules.Sys.Infrastructure.Services.Contracts
{
    /// <summary>
    /// Abstraction for accessing strongly-typed configuration.
    /// Isolates Microsoft.Extensions.Options dependency to Infrastructure layer.
    /// </summary>
    /// <typeparam name="T">Configuration type</typeparam>
    public interface IAppConfiguration<T> where T : class, new()
    {
        /// <summary>
        /// Get current configuration value.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Get configuration value or default if not configured.
        /// </summary>
        T GetValueOrDefault();
    }
}
