using App.Modules.Sys.Infrastructure.Domains.Diagnostics.Implementations;
using App.Modules.Sys.Shared.Services;

namespace App.Modules.Sys.Infrastructure.Domains.Diagnostics;

/// <summary>
/// Registry service for smoke tests.
/// Manages discovery, execution, and results of infrastructure smoke tests.
/// Stateful singleton - maintains in-memory test registry and execution results.
/// </summary>
public interface ISmokeTestRegistryService : 
    IHasRegistryService      // Mark as registry service (stateful singleton)
{
    /// <summary>
    /// Discovers all ISmokeTest implementations from DI container.
    /// Call this after DI container is built but before running tests.
    /// </summary>
    /// <param name="serviceProvider">Service provider to resolve test instances.</param>
    void DiscoverTests(IServiceProvider serviceProvider);

    /// <summary>
    /// Executes all discovered tests in dependency order.
    /// Tests with failed dependencies are skipped.
    /// Results are cached for subsequent GetLastResults() calls.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Domain execution result (not DTO).</returns>
    Task<SmokeTestExecutionResult> RunAllTestsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets cached results from last execution.
    /// Returns empty result if tests have never been run.
    /// </summary>
    /// <returns>Domain execution result (not DTO).</returns>
    SmokeTestExecutionResult GetLastResults();

    /// <summary>
    /// Gets test results filtered by category.
    /// Uses last execution results.
    /// </summary>
    /// <param name="category">Category to filter by (supports hierarchy: "Connection/Database").</param>
    /// <returns>List of domain test results (not DTOs).</returns>
    IReadOnlyList<SmokeTestResult> GetResultsByCategory(string category);
}
