using Microsoft.Extensions.Logging;

namespace App.Modules.Base.Substrate.Models.Contracts
{
    /// <summary>
    /// Contract for Diagnostic Messages within system.
    /// </summary>
    public interface IHasLogLevel
    {
        /// <summary>
        /// Gets or sets the Diagnostic Log Level.
        /// </summary>
        LogLevel Level { get; set; }
    }
}
