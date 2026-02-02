using App.Modules.Sys.Shared.Models.Enums;

namespace App.Modules.Sys.Shared.Models
{
    /// <summary>
    /// Contract for Diagnostic Messages within system.
    /// </summary>
    public interface IHasTraceLevel
    {
        /// <summary>
        /// Gets or sets the Diagnostic Log Level.
        /// </summary>
        TraceLevel Level { get; set; }
    }
}
