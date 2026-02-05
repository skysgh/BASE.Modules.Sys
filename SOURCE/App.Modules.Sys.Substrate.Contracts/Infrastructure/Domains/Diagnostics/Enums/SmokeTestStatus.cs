namespace App.Modules.Sys.Infrastructure.Domains.Diagnostics.Enums;

/// <summary>
/// Smoke test status values.
/// </summary>
public enum SmokeTestStatus
{
    /// <summary>
    /// Test not yet run.
    /// </summary>
    Pending = 0,

    /// <summary>
    /// Test passed - system operational.
    /// </summary>
    Pass = 1,

    /// <summary>
    /// Test passed with warnings - non-critical issues detected.
    /// </summary>
    Warning = 2,

    /// <summary>
    /// Test failed - system may not operate correctly.
    /// </summary>
    Fail = 3,

    /// <summary>
    /// Test skipped due to dependency failure.
    /// </summary>
    Skipped = 4
}


