using App.Modules.Sys.Domain.ReferenceData;
using Microsoft.EntityFrameworkCore;

namespace App.Modules.Sys.Infrastructure.Storage.RDMS.EF.Configuration.Seeding.ReferenceData;

/// <summary>
/// Seed data for SystemLanguage reference data.
/// 
/// Separated from schema configuration per Single Responsibility Principle.
/// Schema defines structure, seeding defines initial data.
/// 
/// Note: IsDefault here is for system-wide fallback only.
/// Workspace/User defaults come from Settings with key "Language.Default".
/// </summary>
public static class SystemLanguageSeeder
{
    /// <summary>
    /// Apply seed data to the model builder.
    /// Called during OnModelCreating.
    /// </summary>
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SystemLanguage>().HasData(
            new SystemLanguage
            {
                Id = new Guid("10000000-0000-0000-0000-000000000001"),
                Key = "en",
                Title = "English",
                NativeName = "English",
                Description = "English (United States)",
                Enabled = true,
                IsDefault = true,  // System-wide fallback only
                DisplayOrderHint = 1
            },
            new SystemLanguage
            {
                Id = new Guid("10000000-0000-0000-0000-000000000002"),
                Key = "es",
                Title = "Spanish",
                NativeName = "Español",
                Description = "Spanish (Spain)",
                Enabled = true,
                IsDefault = false,
                DisplayOrderHint = 2
            },
            new SystemLanguage
            {
                Id = new Guid("10000000-0000-0000-0000-000000000003"),
                Key = "fr",
                Title = "French",
                NativeName = "Français",
                Description = "French (France)",
                Enabled = true,
                IsDefault = false,
                DisplayOrderHint = 3
            },
            new SystemLanguage
            {
                Id = new Guid("10000000-0000-0000-0000-000000000004"),
                Key = "de",
                Title = "German",
                NativeName = "Deutsch",
                Description = "German (Germany)",
                Enabled = true,
                IsDefault = false,
                DisplayOrderHint = 4
            },
            new SystemLanguage
            {
                Id = new Guid("10000000-0000-0000-0000-000000000005"),
                Key = "zh",
                Title = "Chinese",
                NativeName = "中文",
                Description = "Chinese (Simplified)",
                Enabled = false,  // Inactive until translations ready
                IsDefault = false,
                DisplayOrderHint = 5
            }
        );
    }
}
