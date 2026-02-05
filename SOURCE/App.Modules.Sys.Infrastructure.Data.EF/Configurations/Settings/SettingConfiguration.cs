using App.Modules.Sys.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Data.EF.Configurations.Settings;

/// <summary>
/// EF Core configuration for Setting entity.
/// Enforces composite unique constraints and indexes for hierarchical queries.
/// </summary>
internal sealed class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        // Table
        builder.ToTable("Settings", "config");

        // Primary Key
        builder.HasKey(e => e.Id);

        // Unique Constraint: Key must be unique within scope+workspace+user
        builder.HasIndex(e => new { e.Scope, e.WorkspaceId, e.UserId, e.Key })
            .IsUnique()
            .HasDatabaseName("IX_Settings_Scope_Workspace_User_Key_Unique");

        // Performance Indexes
        builder.HasIndex(e => new { e.Scope, e.Key })
            .HasDatabaseName("IX_Settings_Scope_Key");

        builder.HasIndex(e => new { e.WorkspaceId, e.Key })
            .HasDatabaseName("IX_Settings_Workspace_Key");

        builder.HasIndex(e => new { e.UserId, e.Key })
            .HasDatabaseName("IX_Settings_User_Key");

        builder.HasIndex(e => e.Category)
            .HasDatabaseName("IX_Settings_Category");

        builder.HasIndex(e => e.IsLocked)
            .HasDatabaseName("IX_Settings_IsLocked");

        // Properties
        builder.Property(e => e.Key)
            .IsRequired()
            .HasMaxLength(200)
            .IsUnicode(false); // ASCII keys for performance

        builder.Property(e => e.Value)
            .IsRequired()
            .HasMaxLength(4000); // Support JSON values

        builder.Property(e => e.ValueType)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder.Property(e => e.Scope)
            .IsRequired()
            .HasConversion<int>(); // Store as int in DB

        builder.Property(e => e.IsLocked)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.Description)
            .HasMaxLength(1000);

        builder.Property(e => e.Category)
            .HasMaxLength(100);

        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(e => e.UpdatedAt)
            .IsRequired(false);

        // Seed Data - System Baseline Settings
        builder.HasData(
            // Appearance
            new Setting
            {
                Id = new Guid("20000000-0000-0000-0000-000000000001"),
                Key = "theme",
                Value = "light",
                ValueType = "string",
                Scope = SettingScope.System,
                Category = "Appearance",
                Description = "Default UI theme",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Setting
            {
                Id = new Guid("20000000-0000-0000-0000-000000000002"),
                Key = "language",
                Value = "en",
                ValueType = "string",
                Scope = SettingScope.System,
                Category = "Localization",
                Description = "Default UI language code",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Data Display
            new Setting
            {
                Id = new Guid("20000000-0000-0000-0000-000000000003"),
                Key = "pageSize",
                Value = "20",
                ValueType = "int",
                Scope = SettingScope.System,
                Category = "DataDisplay",
                Description = "Default page size for lists",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Setting
            {
                Id = new Guid("20000000-0000-0000-0000-000000000004"),
                Key = "dateFormat",
                Value = "yyyy-MM-dd",
                ValueType = "string",
                Scope = SettingScope.System,
                Category = "Localization",
                Description = "Default date format",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Setting
            {
                Id = new Guid("20000000-0000-0000-0000-000000000005"),
                Key = "timeFormat",
                Value = "HH:mm:ss",
                ValueType = "string",
                Scope = SettingScope.System,
                Category = "Localization",
                Description = "Default time format",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            // Security
            new Setting
            {
                Id = new Guid("20000000-0000-0000-0000-000000000006"),
                Key = "sessionTimeout",
                Value = "30",
                ValueType = "int",
                Scope = SettingScope.System,
                Category = "Security",
                Description = "Session timeout in minutes",
                IsLocked = true, // System admin locked - cannot override
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
