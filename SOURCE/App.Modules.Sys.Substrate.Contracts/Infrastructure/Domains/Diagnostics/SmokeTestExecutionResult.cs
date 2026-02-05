using App.Modules.Sys.Infrastructure.Domains.Diagnostics.Implementations;

namespace App.Modules.Sys.Infrastructure.Domains.Diagnostics;

/// <summary>
/// Domain object representing smoke test execution results.
/// Used within Infrastructure layer only - NOT exposed via API.
/// Application layer maps this to DTOs for external consumption.
/// </summary>
public class SmokeTestExecutionResult
{
    /// <summary>
    /// When tests were executed.
    /// </summary>
    public DateTime ExecutedAt { get; set; }

    /// <summary>
    /// Total time to execute all tests.
    /// </summary>
    public TimeSpan TotalDuration { get; set; }

    /// <summary>
    /// Individual test results.
    /// </summary>
    public List<SmokeTestResult> Results { get; set; } = new();

    /// <summary>
    /// Total number of tests executed.
    /// </summary>
    public int TotalTests { get; set; }

    /// <summary>
    /// Number of tests that passed.
    /// </summary>
    public int PassedTests { get; set; }

    /// <summary>
    /// Number of tests that failed.
    /// </summary>
    public int FailedTests { get; set; }

    /// <summary>
    /// Number of tests with warnings.
    /// </summary>
    public int WarningTests { get; set; }

    /// <summary>
    /// Number of tests skipped due to dependency failures.
    /// </summary>
    public int SkippedTests { get; set; }

    /// <summary>
    /// Number of critical tests that failed.
    /// </summary>
    public int CriticalFailures { get; set; }

    /// <summary>
    /// Test results grouped by category.
    /// </summary>
    public Dictionary<string, int> ResultsByCategory { get; set; } = new();
}
