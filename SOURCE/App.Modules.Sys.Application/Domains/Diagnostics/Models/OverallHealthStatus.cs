namespace App.Modules.Sys.Application.Domains.Diagnostics.Models;

/// <summary>
/// Overall system health status (for CI/CD pipelines).
/// </summary>
public enum OverallHealthStatus
{
    /// <summary>
    /// All tests passed or only non-critical warnings.
    /// System is fully operational.
    /// </summary>
    Healthy = 0,

    /// <summary>
    /// Non-critical tests failed, but system can operate.
    /// Manual review recommended.
    /// </summary>
    Degraded = 1,

    /// <summary>
    /// Critical tests failed.
    /// System may not operate correctly.
    /// </summary>
    Unhealthy = 2,

    /// <summary>
    /// Tests have not been run yet.
    /// </summary>
    Unknown = 3
}
