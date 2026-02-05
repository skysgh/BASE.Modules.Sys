using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.DbContexts.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Infrastructure.Data.EF.Seeding
{
    /// <summary>
    /// Main entry point for Sys module database seeding.
    /// Orchestrates both reference data (code) and demo data (YAML/JSON).
    /// </summary>
    /// <remarks>
    /// Design:
    /// - Run migrations first (ensure schema exists)
    /// - Seed reference data (required for system operation)
    /// - Seed demo data (optional, for development/testing)
    /// - All seeding is idempotent (safe to run multiple times)
    /// 
    /// Demo data path resolution (priority order):
    /// 1. Host/SeedData/ folder (production reality)
    /// 2. SEEDDATA_PATH environment variable (deployment flexibility)
    /// 3. Assembly SeedData/ folder (development convenience)
    /// 
    /// File format support:
    /// - .yml (YAML) - Human-readable, great for configuration
    /// - .json (JSON) - Standard, tooling support
    /// 
    /// Usage in Startup:
    /// <code>
    /// using (var scope = app.ApplicationServices.CreateScope())
    /// {
    ///     var seeder = scope.ServiceProvider.GetRequiredService&lt;SysModuleSeeder&gt;();
    ///     await seeder.SeedAsync(seedDemoData: env.IsDevelopment());
    /// }
    /// </code>
    /// </remarks>
    [SuppressMessage("Performance", "CA1848:Use the LoggerMessage delegates", 
        Justification = "Simple logging in seeder - performance not critical")]
    public class SysModuleSeeder
    {
        private readonly ModuleDbContext _context;
        private readonly ILogger<SysModuleSeeder>? _logger;

        /// <summary>
        /// Create Sys module seeder.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="logger">Optional logger</param>
        public SysModuleSeeder(
            ModuleDbContext context,
            ILogger<SysModuleSeeder>? logger = null)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Run all seeding operations.
        /// </summary>
        /// <param name="runMigrations">Whether to run EF migrations first</param>
        /// <param name="seedDemoData">Whether to seed demo/test data</param>
        /// <param name="ct">Cancellation token</param>
        public async Task SeedAsync(
            bool runMigrations = true,
            bool seedDemoData = true,
            CancellationToken ct = default)
        {
            try
            {
                // Step 1: Ensure database schema exists
                if (runMigrations)
                {
                    _logger?.LogInformation("Running database migrations...");
                    await _context.Database.MigrateAsync(ct);
                    _logger?.LogInformation("Database migrations completed");
                }

                // Step 2: Seed reference data (always)
                _logger?.LogInformation("Seeding reference data...");
                var referenceSeeder = new ReferenceDataSeeder(_context);
                await referenceSeeder.SeedAsync(ct);
                _logger?.LogInformation("Reference data seeded");

                // Step 3: Seed demo data (optional)
                if (seedDemoData)
                {
                    _logger?.LogInformation("Seeding demo data from YAML...");
                    var yamlSeeder = new YamlDataSeeder(_context);
                    await yamlSeeder.SeedAsync(ct);
                    _logger?.LogInformation("Demo data seeded");
                }

                _logger?.LogInformation("Database seeding completed successfully");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Database seeding failed");
                throw;
            }
        }

        /// <summary>
        /// Seed only reference data (skip demo data).
        /// Use for production environments.
        /// </summary>
        public async Task SeedReferenceDataOnlyAsync(CancellationToken ct = default)
        {
            await SeedAsync(
                runMigrations: true,
                seedDemoData: false,
                ct: ct);
        }
    }
}
