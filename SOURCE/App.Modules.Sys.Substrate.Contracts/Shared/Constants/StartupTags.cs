namespace App.Modules.Sys.Shared.Constants;

/// <summary>
/// Standard tags for startup log entries.
/// Used for filtering and categorizing startup diagnostics.
/// </summary>
public static class StartupTags
{
    /// <summary>
    /// Module discovery and initialization.
    /// </summary>
    public const string Module = "Module";

    /// <summary>
    /// Service registration (DI container).
    /// </summary>
    public const string Service = "Service";

    /// <summary>
    /// Database context registration.
    /// </summary>
    public const string DbContext = "DbContext";

    /// <summary>
    /// Entity/schema configuration.
    /// </summary>
    public const string Schema = "Schema";

    /// <summary>
    /// Database connection testing.
    /// </summary>
    public const string Connection = "Connection";

    /// <summary>
    /// EF Core migration application.
    /// </summary>
    public const string Migration = "Migration";

    /// <summary>
    /// Middleware registration.
    /// </summary>
    public const string Middleware = "Middleware";

    /// <summary>
    /// Configuration loading/binding.
    /// </summary>
    public const string Configuration = "Configuration";

    /// <summary>
    /// Cache initialization.
    /// </summary>
    public const string Cache = "Cache";

    /// <summary>
    /// API/Controller registration.
    /// </summary>
    public const string Api = "API";

    /// <summary>
    /// General initialization step.
    /// </summary>
    public const string Initialization = "Initialization";

    /// <summary>
    /// Error/failure during startup.
    /// </summary>
    public const string Error = "Error";
}
