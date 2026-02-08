using App.Modules.Sys.Domain.Domains.Permissions.Models;
using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.DbContexts.Implementations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Modules.Sys.Infrastructure.Storage.RDMS.EF.Configuration.Seeding
{
    /// <summary>
    /// Seeds reference data (SystemPermissions) from code.
    /// This is version-controlled, type-safe reference data that should always exist.
    /// </summary>
    /// <remarks>
    /// Reference data characteristics:
    /// - Required for system operation
    /// - Version controlled with code
    /// - Type-safe definitions
    /// - Should NOT be modified by users
    /// - Idempotent (safe to run multiple times)
    /// </remarks>
    public class ReferenceDataSeeder
    {
        private readonly ModuleDbContext _context;

        /// <summary>
        /// Create reference data seeder.
        /// </summary>
        /// <param name="context">Database context</param>
        public ReferenceDataSeeder(ModuleDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Seed all reference data.
        /// </summary>
        public async Task SeedAsync(CancellationToken ct = default)
        {
            await SeedSystemPermissionsAsync(ct);
            await _context.SaveChangesAsync(ct);
        }

        /// <summary>
        /// Seed SystemPermissions (runtime authorization).
        /// These are the baseline permissions the system needs to function.
        /// </summary>
        private async Task SeedSystemPermissionsAsync(CancellationToken ct)
        {
            var permissions = GetDefaultSystemPermissions();

            foreach (var permission in permissions)
            {
                // Idempotent - only add if doesn't exist
                var exists = await _context.SystemPermissions
                    .AnyAsync(p => p.Key == permission.Key, ct);

                if (!exists)
                {
                    _context.SystemPermissions.Add(permission);
                }
            }
        }

        /// <summary>
        /// Define default system permissions.
        /// These are code-based for type safety and version control.
        /// </summary>
        private static List<SystemPermission> GetDefaultSystemPermissions()
        {
            return new List<SystemPermission>
            {
                // System Administration
                new SystemPermission
                {
                    Key = "System.Configure",
                    Title = "Configure System",
                    Description = "Modify system-wide configuration settings",
                    Category = "System"
                },
                new SystemPermission
                {
                    Key = "System.ViewLogs",
                    Title = "View System Logs",
                    Description = "Access system diagnostic and audit logs",
                    Category = "System"
                },
                new SystemPermission
                {
                    Key = "System.ManageModules",
                    Title = "Manage Modules",
                    Description = "Enable, disable, and configure system modules",
                    Category = "System"
                },

                // Database Management
                new SystemPermission
                {
                    Key = "Database.Migrate",
                    Title = "Run Database Migrations",
                    Description = "Execute database schema migrations",
                    Category = "Database"
                },
                new SystemPermission
                {
                    Key = "Database.Backup",
                    Title = "Backup Database",
                    Description = "Create database backups",
                    Category = "Database"
                },
                new SystemPermission
                {
                    Key = "Database.Restore",
                    Title = "Restore Database",
                    Description = "Restore database from backup",
                    Category = "Database"
                },

                // Settings Management
                new SystemPermission
                {
                    Key = "Settings.Edit",
                    Title = "Edit Settings",
                    Description = "Modify system, workspace, and user settings",
                    Category = "Settings"
                },
                new SystemPermission
                {
                    Key = "Settings.Lock",
                    Title = "Lock Settings",
                    Description = "Prevent settings from being overridden",
                    Category = "Settings"
                },
                new SystemPermission
                {
                    Key = "Settings.Reset",
                    Title = "Reset Settings",
                    Description = "Reset settings to default values",
                    Category = "Settings"
                },

                // User Management
                new SystemPermission
                {
                    Key = "Users.Create",
                    Title = "Create Users",
                    Description = "Create new user accounts",
                    Category = "Users"
                },
                new SystemPermission
                {
                    Key = "Users.Edit",
                    Title = "Edit Users",
                    Description = "Modify existing user accounts",
                    Category = "Users"
                },
                new SystemPermission
                {
                    Key = "Users.Delete",
                    Title = "Delete Users",
                    Description = "Remove user accounts",
                    Category = "Users"
                },
                new SystemPermission
                {
                    Key = "Users.ViewAll",
                    Title = "View All Users",
                    Description = "View all user accounts in the system",
                    Category = "Users"
                },

                // Permission Management
                new SystemPermission
                {
                    Key = "Permissions.Grant",
                    Title = "Grant Permissions",
                    Description = "Assign system permissions to users",
                    Category = "Permissions"
                },
                new SystemPermission
                {
                    Key = "Permissions.Revoke",
                    Title = "Revoke Permissions",
                    Description = "Remove system permissions from users",
                    Category = "Permissions"
                },
                new SystemPermission
                {
                    Key = "Permissions.View",
                    Title = "View Permissions",
                    Description = "View system permission definitions",
                    Category = "Permissions"
                },

                // Identity Provider Management
                new SystemPermission
                {
                    Key = "Identity.ConfigureProviders",
                    Title = "Configure Identity Providers",
                    Description = "Setup OAuth, SAML, and other authentication providers",
                    Category = "Identity"
                },
                new SystemPermission
                {
                    Key = "Identity.LinkAccounts",
                    Title = "Link Identity Providers",
                    Description = "Connect user accounts to external identity providers",
                    Category = "Identity"
                },

                // System Administration (Replaces IsSystemAdmin flag)
                new SystemPermission
                {
                    Key = "System.Admin",
                    Title = "System Administrator",
                    Description = "Full system administration access (replaces IsSystemAdmin flag)",
                    Category = "System"
                }
            };
        }
    }
}
