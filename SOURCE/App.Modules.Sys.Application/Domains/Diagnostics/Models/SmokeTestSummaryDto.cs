namespace App.Modules.Sys.Application.Domains.Diagnostics.Models;

/// <summary>
/// DTO for smoke test execution summary.
/// Safe for API exposure - maps from Infrastructure.Diagnostics.SmokeTestExecutionResult.
/// Provides overall health status and aggregated statistics.
/// </summary>
public record SmokeTestSummaryDto
{
    /// <summary>
    /// Overall system health status.
    /// Values: "Healthy", "Degraded", "Unhealthy", "Unknown"
    /// </summary>
    public string OverallStatus { get; init; } = "Unknown";

    /// <summary>
    /// Total number of tests executed.
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
    /// If > 0, system may not operate correctly.
    /// </summary>
    public int CriticalFailures { get; init; }

    /// <summary>
    /// Total execution time for all tests.
    /// </summary>
    public TimeSpan TotalDuration { get; init; }

    /// <summary>
    /// When tests were executed (UTC).
    /// </summary>
    public DateTime? LastRunAt { get; init; }

    /// <summary>
    /// Individual test results.
    /// </summary>
    public List<SmokeTestResultDto> Results { get; init; } = new();

    /// <summary>
    /// Test counts grouped by category.
    /// Example: {"Configuration": 1, "Connection/Database": 2, "Database/Migration": 1}
    /// </summary>
    public Dictionary<string, int> ResultsByCategory { get; init; } = new();
}
