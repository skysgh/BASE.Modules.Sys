using App.Modules.Sys.Infrastructure.Domains.Diagnostics;
using App.Modules.Sys.Infrastructure.Domains.Diagnostics.Enums;
using App.Modules.Sys.Infrastructure.Domains.Diagnostics.Implementations;
using System.Collections.Concurrent;

namespace App.Modules.Sys.Infrastructure.Services.Implementations;

/// <summary>
/// Registry service for smoke tests.
/// Singleton that maintains in-memory test registry and execution results.
/// Implements IHasRegistryService to mark as stateful singleton.
/// </summary>
internal sealed class SmokeTestRegistryService : ISmokeTestRegistryService
{
    private readonly ConcurrentBag<ISmokeTest> _registeredTests = new();
    private SmokeTestExecutionResult? _lastResults;
    private readonly object _lock = new();

    public void DiscoverTests(IServiceProvider serviceProvider)
    {
        var testsObj = serviceProvider.GetService(typeof(IEnumerable<ISmokeTest>));
        
        if (testsObj is IEnumerable<ISmokeTest> tests)
        {
            foreach (var test in tests)
            {
                _registeredTests.Add(test);
            }
        }
    }

    public async Task<SmokeTestExecutionResult> RunAllTestsAsync(CancellationToken cancellationToken = default)
    {
        var allTests = _registeredTests.ToList();
        
        if (allTests.Count == 0)
        {
            return new SmokeTestExecutionResult
            {
                ExecutedAt = DateTime.UtcNow,
                TotalTests = 0
            };
        }

        var orderedTests = TopologicalSort(allTests);
        var results = new ConcurrentBag<SmokeTestResult>();
        var failedTestIds = new HashSet<string>();
        var startTime = DateTime.UtcNow;

        foreach (var test in orderedTests)
        {
            var shouldSkip = test.Dependencies.Any(depId => failedTestIds.Contains(depId));
            
            if (shouldSkip)
            {
                var skippedResult = new SmokeTestResult();
                skippedResult.Initialize(test.TestId, SmokeTestStatus.Skipped, "Dependency failed");
                skippedResult.StartUtc = DateTime.UtcNow;
                skippedResult.Complete();
                results.Add(skippedResult);
                failedTestIds.Add(test.TestId);
                continue;
            }

            var result = await test.ExecuteAsync(cancellationToken);
            results.Add(result);

            if (result.Status == SmokeTestStatus.Fail)
            {
                failedTestIds.Add(test.TestId);
            }
        }

        var executionResult = BuildExecutionResult(results.ToList(), startTime, allTests);
        
        lock (_lock)
        {
            _lastResults = executionResult;
        }

        return executionResult;
    }

    public SmokeTestExecutionResult GetLastResults()
    {
        lock (_lock)
        {
            return _lastResults ?? new SmokeTestExecutionResult
            {
                ExecutedAt = DateTime.UtcNow,
                TotalTests = 0
            };
        }
    }

    public IReadOnlyList<SmokeTestResult> GetResultsByCategory(string category)
    {
        var last = GetLastResults();
        
        return last.Results
            .Where(r => r.Tags.Any(t => t.StartsWith(category, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    private static List<ISmokeTest> TopologicalSort(List<ISmokeTest> tests)
    {
        var sorted = new List<ISmokeTest>();
        var visited = new HashSet<string>();
        var visiting = new HashSet<string>();

        void Visit(ISmokeTest test)
        {
            if (visited.Contains(test.TestId))
            {
                return;
            }

            if (visiting.Contains(test.TestId))
            {
                return;
            }

            visiting.Add(test.TestId);

            foreach (var depId in test.Dependencies)
            {
                var depTest = tests.FirstOrDefault(t => t.TestId == depId);
                if (depTest != null)
                {
                    Visit(depTest);
                }
            }

            visiting.Remove(test.TestId);
            visited.Add(test.TestId);
            sorted.Add(test);
        }

        foreach (var test in tests)
        {
            Visit(test);
        }

        return sorted;
    }

    private static SmokeTestExecutionResult BuildExecutionResult(
        List<SmokeTestResult> results,
        DateTime startTime,
        List<ISmokeTest> allTests)
    {
        var criticalFailures = 0;
        foreach (var result in results.Where(r => r.Status == SmokeTestStatus.Fail))
        {
            var test = allTests.FirstOrDefault(t => t.TestId == result.TestId);
            if (test?.IsCritical == true)
            {
                criticalFailures++;
            }
        }

        var byCategory = new Dictionary<string, int>();
        foreach (var result in results)
        {
            var test = allTests.FirstOrDefault(t => t.TestId == result.TestId);
            if (test != null)
            {
                var category = test.Category;
                if (byCategory.TryGetValue(category, out var count))
                {
                    byCategory[category] = count + 1;
                }
                else
                {
                    byCategory[category] = 1;
                }
            }
        }

        return new SmokeTestExecutionResult
        {
            ExecutedAt = startTime,
            TotalDuration = DateTime.UtcNow - startTime,
            Results = results,
            TotalTests = results.Count,
            PassedTests = results.Count(r => r.Status == SmokeTestStatus.Pass),
            WarningTests = results.Count(r => r.Status == SmokeTestStatus.Warning),
            FailedTests = results.Count(r => r.Status == SmokeTestStatus.Fail),
            SkippedTests = results.Count(r => r.Status == SmokeTestStatus.Skipped),
            CriticalFailures = criticalFailures,
            ResultsByCategory = byCategory
        };
    }
}
