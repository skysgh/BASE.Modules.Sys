namespace App.Modules.Sys.Shared.Constants;

/// <summary>
/// Constants for context keys used in HttpContext.Items and RouteValues.
/// Prevents magic string bugs.
/// </summary>
public static class ContextKeys
{
    /// <summary>
    /// HttpContext.Items key for resolved workspace identifier.
    /// </summary>
    public const string WorkspaceId = "WorkspaceId";

    /// <summary>
    /// HttpContext.Items key for current user identifier.
    /// </summary>
    public const string UserId = "UserId";

    /// <summary>
    /// HttpContext.Items key for current session identifier.
    /// </summary>
    public const string SessionId = "SessionId";

    /// <summary>
    /// HttpContext.Items key for request correlation identifier.
    /// </summary>
    public const string CorrelationId = "CorrelationId";
}

/// <summary>
/// Constants for metadata keys used in diagnostic entries.
/// </summary>
public static class MetadataKeys
{
    /// <summary>
    /// Duration metadata key.
    /// </summary>
    public const string Duration = "Duration";

    /// <summary>
    /// Exception type metadata key.
    /// </summary>
    public const string ExceptionType = "ExceptionType";

    /// <summary>
    /// Exception message metadata key.
    /// </summary>
    public const string ExceptionMessage = "ExceptionMessage";

    /// <summary>
    /// Connection string metadata key (sanitized).
    /// </summary>
    public const string ConnectionString = "ConnectionString";

    /// <summary>
    /// Database schema metadata key.
    /// </summary>
    public const string Schema = "Schema";

    /// <summary>
    /// Migration name metadata key.
    /// </summary>
    public const string MigrationName = "MigrationName";

    /// <summary>
    /// Service type (interface) metadata key.
    /// </summary>
    public const string ServiceType = "ServiceType";

    /// <summary>
    /// Implementation type metadata key.
    /// </summary>
    public const string ImplementationType = "ImplementationType";
}

/// <summary>
/// Constants for workspace-related values.
/// </summary>
public static class WorkspaceConstants
{
    /// <summary>
    /// Default workspace identifier when none is resolved.
    /// </summary>
    public const string DefaultWorkspace = "default";

    /// <summary>
    /// Reserved workspace identifiers that cannot be used as workspace names.
    /// </summary>
    public static readonly string[] ReservedWorkspaceNames = new[]
    {
        "api",
        "admin",
        "system",
        "www",
        "mail",
        "default",
        "public"
    };
}
