using App.Modules.Sys.Infrastructure.Domains.Diagnostics;
using App.Modules.Sys.Infrastructure.Domains.Diagnostics.Enums;
using App.Modules.Sys.Infrastructure.Domains.Diagnostics.Implementations;
using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.DbContexts.Implementations;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Diagnostics.Tests;

/// <summary>
/// Validates database connectivity.
/// Tests that the application can connect to the configured database.
/// </summary>
public class DatabaseConnectionSmokeTest : SmokeTestBase
{
    private readonly ModuleDbContext _dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public DatabaseConnectionSmokeTest(ModuleDbContext dbContext)
    {
        _dbContext = dbContext;
        Title = "Database Connection Test";
        Description = "Validates that application can connect to the sysmdl database";
    }

    /// <inheritdoc/>
    public override string TestId => "Database.Connection";

    /// <inheritdoc/>
    public override string Category => "Connection/Database";

    /// <inheritdoc/>
    public override IEnumerable<string> Dependencies => new[] { "Configuration.Basic" };

    /// <inheritdoc/>
    public override bool IsCritical => true;

    /// <inheritdoc/>
    public override int TimeoutSeconds => 10;

    /// <inheritdoc/>
    protected override async Task<SmokeTestResult> ExecuteTestAsync(CancellationToken cancellationToken)
    {
        var details = new Dictionary<string, object>();

        try
        {
            // Test 1: DbContext exists
            if (_dbContext == null)
            {
                return Fail("ModuleDbContext is null - dependency injection failed");
            }

            // Get connection details (sanitized)
            var connectionString = _dbContext.Database.GetConnectionString();
            if (!string.IsNullOrEmpty(connectionString))
            {
                // Sanitize - only show server and database, hide credentials
                var sanitized = SanitizeConnectionString(connectionString);
                details["ConnectionString"] = sanitized;
            }

            details["Provider"] = _dbContext.Database.ProviderName ?? "Unknown";

            // Test 2: Can connect to database
            var canConnect = await _dbContext.Database.CanConnectAsync(cancellationToken);

            if (!canConnect)
            {
                details["CanConnect"] = false;
                return Fail("Cannot connect to database - check connection string and database availability", null, details);
            }

            details["CanConnect"] = true;

            // Test 3: Database exists (if can connect, it exists)
            details["DatabaseExists"] = true;

            // Test 4: Schema information
            try
            {
                // Simple query to verify database is operational
                var tableCount = await _dbContext.Database
                    .SqlQueryRaw<int>("SELECT COUNT(*) as Value FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'sysmdl'")
                    .FirstOrDefaultAsync(cancellationToken);

                details["TablesInSchema"] = tableCount;

                if (tableCount == 0)
                {
                    return Warn("Database connection successful but no tables found in sysmdl schema - migrations may not have run", details);
                }
            }
            catch (Exception ex)
            {
                // Schema query failed but connection worked - non-critical
                details["SchemaQueryError"] = ex.Message;
            }

            return Pass("Database connection successful and operational", details);
        }
        catch (Exception ex)
        {
            return Fail($"Database connection test failed: {ex.Message}", ex, details);
        }
    }

    /// <summary>
    /// Sanitizes connection string for logging (removes passwords).
    /// </summary>
    private static string SanitizeConnectionString(string connectionString)
    {
        // Basic sanitization - remove password/credentials
        var parts = connectionString.Split(';');
        var sanitized = parts
            .Where(p => !p.Contains("Password", StringComparison.OrdinalIgnoreCase) &&
                       !p.Contains("Pwd", StringComparison.OrdinalIgnoreCase) &&
                       !p.Contains("User ID", StringComparison.OrdinalIgnoreCase))
            .ToList();

        // Add placeholders for removed parts
        if (parts.Length != sanitized.Count)
        {
            sanitized.Add("Credentials=<REDACTED>");
        }

        return string.Join("; ", sanitized);
    }
}
