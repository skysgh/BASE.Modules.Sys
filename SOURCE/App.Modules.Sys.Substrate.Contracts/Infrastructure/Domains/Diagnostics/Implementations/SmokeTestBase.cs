using App.Modules.Sys.Infrastructure.Domains.Diagnostics;
using App.Modules.Sys.Infrastructure.Domains.Diagnostics.Enums;
using App.Modules.Sys.Shared.Models.Enums;
using App.Modules.Sys.Shared.Models.Implementations;

namespace App.Modules.Sys.Infrastructure.Domains.Diagnostics.Implementations;

/// <summary>
/// Base class for smoke tests - provides common functionality.
/// Handles timing, error catching, timeout enforcement, and dependency validation.
/// Inherits from UniversalDisplayItem for consistent UI rendering.
/// </summary>
public abstract class SmokeTestBase : UniversalDisplayItem, ISmokeTest
{
    /// <inheritdoc/>
    public abstract string TestId { get; }

    /// <inheritdoc/>
    public abstract string Category { get; }

    /// <inheritdoc/>
    public virtual IEnumerable<string> Dependencies => Array.Empty<string>();

    /// <inheritdoc/>
    public virtual bool IsCritical => false;

    /// <inheritdoc/>
    public virtual int TimeoutSeconds => 10; // Default 10s - override for longer operations

    /// <summary>
    /// Constructor - initializes base UniversalDisplayItem properties.
    /// </summary>
    protected SmokeTestBase()
    {
        // Default trace level (will be updated based on test result)
        Level = TraceLevel.Info;
        Tags = new List<string> { "SmokeTest" };
    }

    /// <inheritdoc/>
    public async Task<SmokeTestResult> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var result = new SmokeTestResult();
        result.Initialize(TestId, SmokeTestStatus.Pending, "Executing...");
        result.StartUtc = DateTime.UtcNow;

        try
        {
            // Create timeout cancellation token
            using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(TimeoutSeconds));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCts.Token);

            // Execute the actual test logic with timeout
            result = await ExecuteTestAsync(linkedCts.Token);
            result.TestId = TestId;
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            // Timeout occurred (not user cancellation)
            result = CreateResult(
                SmokeTestStatus.Fail,
                $"Test timed out after {TimeoutSeconds} seconds");
        }
        catch (Exception ex)
        {
            result = CreateResult(
                SmokeTestStatus.Fail,
                $"Test threw exception: {ex.Message}",
                ex);
        }

        result.Complete();
        return result;
    }

    /// <summary>
    /// Implement test logic here.
    /// Exceptions are automatically caught and converted to Fail status.
    /// Respect cancellationToken for timeout enforcement.
    /// </summary>
    protected abstract Task<SmokeTestResult> ExecuteTestAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Helper to create a result with common setup.
    /// </summary>
    protected SmokeTestResult CreateResult(
        SmokeTestStatus status,
        string message,
        Exception? ex = null,
        Dictionary<string, object>? details = null)
    {
        var result = new SmokeTestResult();
        result.Initialize(TestId, status, message);
        result.Exception = ex;

        if (details != null)
        {
            foreach (var kvp in details)
            {
                result.Metadata[kvp.Key] = kvp.Value;
            }
        }

        return result;
    }

    /// <summary>
    /// Helper to create a passing result.
    /// </summary>
    protected SmokeTestResult Pass(string message, Dictionary<string, object>? details = null)
    {
        return CreateResult(SmokeTestStatus.Pass, message, null, details);
    }

    /// <summary>
    /// Helper to create a warning result.
    /// </summary>
    protected SmokeTestResult Warn(string message, Dictionary<string, object>? details = null)
    {
        return CreateResult(SmokeTestStatus.Warning, message, null, details);
    }

    /// <summary>
    /// Helper to create a failure result.
    /// </summary>
    protected SmokeTestResult Fail(string message, Exception? ex = null, Dictionary<string, object>? details = null)
    {
        return CreateResult(SmokeTestStatus.Fail, message, ex, details);
    }
}


