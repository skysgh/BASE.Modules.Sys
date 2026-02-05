using App.Modules.Sys.Infrastructure.Domains.Diagnostics.Enums;
using App.Modules.Sys.Shared.Models;
using App.Modules.Sys.Shared.Models.Enums;
using App.Modules.Sys.Shared.Models.Implementations;

namespace App.Modules.Sys.Infrastructure.Domains.Diagnostics.Implementations;

/// <summary>
/// Result of a smoke test execution.
/// Inherits from UniversalDisplayItem for consistent UI rendering.
/// Implements marker interfaces for validation and reflection.
/// </summary>
public class SmokeTestResult : UniversalDisplayItem, 
    IHasException
{
    /// <summary>
    /// Test identifier (matches ISmokeTest.TestId).
    /// </summary>
    public string TestId { get; set; } = string.Empty;

    /// <summary>
    /// Overall test status.
    /// </summary>
    public SmokeTestStatus Status { get; set; }

    /// <summary>
    /// When test was executed.
    /// </summary>
    public DateTime? StartUtc { get; set; }

    /// <summary>
    /// When test finished executing.
    /// </summary>
    public DateTime? EndUtc { get; set; }

    /// <summary>
    /// Test execution duration.
    /// </summary>
    public TimeSpan Duration
    {
        get
        {
            if (StartUtc == null || EndUtc == null)
            {
                return TimeSpan.Zero;
            }
            return EndUtc.Value - StartUtc.Value;
        }
    }

    /// <summary>
    /// Exception if test failed.
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    /// Initialize result with test ID and status.
    /// Sets Level based on status.
    /// </summary>
    public void Initialize(string testId, SmokeTestStatus status, string message)
    {
        TestId = testId;
        Status = status;
        Title = testId;
        Description = message;
        StartUtc = DateTime.UtcNow;

        // Map status to TraceLevel
        Level = status switch
        {
            SmokeTestStatus.Pass => TraceLevel.Info,
            SmokeTestStatus.Warning => TraceLevel.Warn,
            SmokeTestStatus.Fail => TraceLevel.Error,
            SmokeTestStatus.Skipped => TraceLevel.Warn,
            _ => TraceLevel.Info
        };

        // Auto-tag
        Tags = new List<string> { "SmokeTest" };
    }

    /// <summary>
    /// Complete the result (set EndUtc, compute duration metadata).
    /// </summary>
    public void Complete()
    {
        EndUtc = DateTime.UtcNow;
        Metadata["Duration"] = $"{Duration.TotalMilliseconds:F0}ms";

        if (Exception != null)
        {
            Metadata["ExceptionType"] = Exception.GetType().Name;
            Metadata["ExceptionMessage"] = Exception.Message;
        }
    }
}


