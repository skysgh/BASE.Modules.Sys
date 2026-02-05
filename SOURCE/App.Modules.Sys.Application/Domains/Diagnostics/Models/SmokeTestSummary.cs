using App.Modules.Sys.Infrastructure.Domains.Diagnostics.Implementations;

namespace App.Modules.Sys.Application.Domains.Diagnostics.Models;

/// <summary>
/// Summary of smoke test execution.
/// </summary>
public record SmokeTestSummary
{
    /// <summary>
    /// Overall system health status.
    /// </summary>
    public OverallHealthStatus OverallStatus { get; init; }

    /// <summary>
    /// Total number of tests.
    /// </summary>
    public int TotalTests { get; init; }

    /// <summary>
    /// Number of tests that passed.
    /// </summary>
    public int PassedTests { get; init; }

    /// <summary>
    /// Number of tests with warnings.
    /// </summary>
    public int WarningTests { get; init; }

    /// <summary>
    /// Number of tests that failed.
    /// </summary>
    public int FailedTests { get; init; }

    /// <summary>
    /// Number of tests skipped (due to dependency failures).
    /// </summary>
    public int SkippedTests { get; init; }

    /// <summary>
    /// Number of critical tests that failed.
    /// </summary>
    public int CriticalFailures { get; init; }

    /// <summary>
    /// Total execution time for all tests.
    /// </summary>
    public TimeSpan TotalDuration { get; init; }

    /// <summary>
    /// When tests were last run.
    /// </summary>
    public DateTime? LastRunAt { get; init; }

    /// <summary>
    /// Individual test results.
    /// </summary>
    public IReadOnlyList<SmokeTestResult> Results { get; init; } = Array.Empty<SmokeTestResult>();

    /// <summary>
    /// Test results grouped by category.
    /// </summary>
    public Dictionary<string, int> ResultsByCategory { get; init; } = new();
}
