using App.Modules.Sys.Infrastructure.Domains.Diagnostics.Implementations;
using Microsoft.Extensions.Configuration;

namespace App.Modules.Sys.Infrastructure.Domains.Diagnostics.Tests;

/// <summary>
/// Validates that application configuration is readable and contains required sections.
/// This is a foundational test - if configuration fails, nothing else will work.
/// </summary>
public class ConfigurationSmokeTest : SmokeTestBase
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ConfigurationSmokeTest(IConfiguration configuration)
    {
        _configuration = configuration;
        Title = "Configuration File Accessibility";
        Description = "Validates appsettings.json is readable and contains required sections";
    }

    /// <inheritdoc/>
    public override string TestId => "Configuration.Basic";

    /// <inheritdoc/>
    public override string Category => "Configuration";

    /// <inheritdoc/>
    public override bool IsCritical => true;

    /// <inheritdoc/>
    public override int TimeoutSeconds => 5;

    /// <inheritdoc/>
    protected override async Task<SmokeTestResult> ExecuteTestAsync(CancellationToken cancellationToken)
    {
        var details = new Dictionary<string, object>();

        try
        {
            // Test 1: Configuration object exists
            if (_configuration == null)
            {
                return Fail("IConfiguration is null - dependency injection failed");
            }

            // Test 2: Can read basic configuration sections
            var requiredSections = new[] { "Logging", "AllowedHosts" };
            var missingSections = new List<string>();

            foreach (var section in requiredSections)
            {
                var sectionExists = _configuration.GetSection(section).Exists();
                details[$"Section:{section}"] = sectionExists ? "Found" : "Missing";

                if (!sectionExists)
                {
                    missingSections.Add(section);
                }
            }

            // Test 3: Environment detection
            var environment = _configuration["Environment"] ?? "Unknown";
            details["Environment"] = environment;

            // Test 4: Connection strings section (may be empty, but section should exist)
            var connStringsSection = _configuration.GetSection("ConnectionStrings");
            details["ConnectionStrings"] = connStringsSection.Exists() ? "Found" : "Missing";

            // Evaluate results
            if (missingSections.Count > 0)
            {
                return Warn(
                    $"Configuration is readable but missing {missingSections.Count} expected sections: {string.Join(", ", missingSections)}",
                    details);
            }

            details["Status"] = "All required configuration sections found";
            return Pass("Configuration is fully accessible and contains expected sections", details);
        }
        catch (Exception ex)
        {
            return Fail($"Configuration validation failed: {ex.Message}", ex, details);
        }
    }
}
