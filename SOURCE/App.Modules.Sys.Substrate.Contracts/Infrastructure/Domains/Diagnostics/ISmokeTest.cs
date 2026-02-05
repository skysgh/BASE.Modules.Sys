using App.Modules.Sys.Infrastructure.Domains.Diagnostics.Implementations;
using App.Modules.Sys.Shared.Models;

namespace App.Modules.Sys.Infrastructure.Domains.Diagnostics;

/// <summary>
/// Contract for smoke tests - validates infrastructure and data integrity.
/// Smoke tests run after startup to ensure system is operational.
/// Inherits from IUniversalDisplayItem for consistent UI rendering.
/// </summary>
public interface ISmokeTest : IUniversalDisplayItem
{
    /// <summary>
    /// Unique identifier for this smoke test.
    /// Used for dependency references and logging.
    /// </summary>
    string TestId { get; }

    /// <summary>
    /// Category for grouping/filtering tests.
    /// Examples: "Configuration", "Connection", "Data", "Schema"
    /// Supports hierarchy: "Connection/Database", "Connection/Cache"
    /// </summary>
    string Category { get; }

    /// <summary>
    /// Test IDs that must pass before this test can run.
    /// Enables dependency-ordered execution.
    /// Example: DatabaseQueryTest depends on DatabaseConnectionTest
    /// </summary>
    IEnumerable<string> Dependencies { get; }

    /// <summary>
    /// Whether this test is critical for system operation.
    /// Critical failures should prevent application startup.
    /// Non-critical failures are warnings only.
    /// </summary>
    bool IsCritical { get; }

    /// <summary>
    /// Maximum execution time before test is cancelled (in seconds).
    /// Default: 10 seconds
    /// Override for longer operations:
    /// - Configuration: 5s
    /// - Connections: 10s
    /// - Migrations: 60s
    /// </summary>
    int TimeoutSeconds { get; }

    /// <summary>
    /// Execute the smoke test with timeout enforcement.
    /// Should be fast - these are smoke tests, not integration tests.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Test result with status and details.</returns>
    Task<SmokeTestResult> ExecuteAsync(CancellationToken cancellationToken = default);
}


