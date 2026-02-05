using App.Modules.Sys.Infrastructure.Domains.Diagnostics;
using App.Modules.Sys.Infrastructure.Domains.Diagnostics.Enums;
using App.Modules.Sys.Infrastructure.Domains.Diagnostics.Implementations;
using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.DbContexts.Implementations;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Diagnostics.Tests;

/// <summary>
/// Validates that database schema is up to date with code.
/// Checks that all code migrations have been applied to the database.
/// </summary>
public class DatabaseMigrationAlignmentTest : SmokeTestBase
{
    private readonly ModuleDbContext _dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public DatabaseMigrationAlignmentTest(ModuleDbContext dbContext)
    {
        _dbContext = dbContext;
        Title = "Database Migration Alignment";
        Description = "Validates that all code migrations have been applied to the database";
    }

    /// <inheritdoc/>
    public override string TestId => "Database.MigrationAlignment";

    /// <inheritdoc/>
    public override string Category => "Database/Migration";

    /// <inheritdoc/>
    public override IEnumerable<string> Dependencies => new[] { "Database.Connection" };

    /// <inheritdoc/>
    public override bool IsCritical => false;

    /// <inheritdoc/>
    public override int TimeoutSeconds => 60;

    /// <inheritdoc/>
    protected override async Task<SmokeTestResult> ExecuteTestAsync(CancellationToken cancellationToken)
    {
        var details = new Dictionary<string, object>();

        try
        {
            // Get all migrations defined in code
            var allMigrations = _dbContext.Database.GetMigrations().ToList();
            details["TotalMigrationsInCode"] = allMigrations.Count;

            // Get migrations that have been applied to database
            var appliedMigrations = (await _dbContext.Database.GetAppliedMigrationsAsync(cancellationToken)).ToList();
            details["AppliedMigrationsInDb"] = appliedMigrations.Count;

            // Get pending migrations (in code but not in DB)
            var pendingMigrations = (await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).ToList();
            details["PendingMigrations"] = pendingMigrations.Count;

            // Log last 5 applied migrations for reference
            var recentMigrations = appliedMigrations.TakeLast(5).ToList();
            for (int i = 0; i < recentMigrations.Count; i++)
            {
                details[$"Recent_{i + 1}"] = recentMigrations[i];
            }

            // Evaluate alignment
            if (pendingMigrations.Count > 0)
            {
                // List pending migrations
                for (int i = 0; i < Math.Min(pendingMigrations.Count, 10); i++)
                {
                    details[$"Pending_{i + 1}"] = pendingMigrations[i];
                }

                return Warn(
                    $"Database has {pendingMigrations.Count} pending migration(s). Run 'dotnet ef database update' to apply them.",
                    details);
            }

            // Check if database has migrations that aren't in code (rolled back?)
            var unknownMigrations = appliedMigrations.Except(allMigrations).ToList();
            if (unknownMigrations.Count > 0)
            {
                details["UnknownMigrations"] = unknownMigrations.Count;
                for (int i = 0; i < Math.Min(unknownMigrations.Count, 5); i++)
                {
                    details[$"Unknown_{i + 1}"] = unknownMigrations[i];
                }

                return Warn(
                    $"Database has {unknownMigrations.Count} migration(s) not found in code. This may indicate a rollback or version mismatch.",
                    details);
            }

            // Perfect alignment
            details["Status"] = "All migrations applied, database schema is up to date";
            details["LatestMigration"] = appliedMigrations.LastOrDefault() ?? "None";

            return Pass("Database schema is fully aligned with code migrations", details);
        }
        catch (Exception ex)
        {
            return Fail($"Migration alignment check failed: {ex.Message}", ex, details);
        }
    }
}
