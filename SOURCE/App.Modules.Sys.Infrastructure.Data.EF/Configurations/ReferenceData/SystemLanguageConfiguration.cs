using App.Modules.Sys.Domain.ReferenceData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Data.EF.Configurations.ReferenceData;

/// <summary>
/// EF Core configuration for SystemLanguage entity.
/// </summary>
internal sealed class SystemLanguageConfiguration : IEntityTypeConfiguration<SystemLanguage>
{
    public void Configure(EntityTypeBuilder<SystemLanguage> builder)
    {
        // Table
        builder.ToTable("SystemLanguages", "refdata");

        // Primary Key
        builder.HasKey(e => e.Id);

        // Indexes
        builder.HasIndex(e => e.Code)
            .IsUnique()
            .HasDatabaseName("IX_SystemLanguages_Code");

        builder.HasIndex(e => e.IsActive)
            .HasDatabaseName("IX_SystemLanguages_IsActive");

        builder.HasIndex(e => e.IsDefault)
            .HasDatabaseName("IX_SystemLanguages_IsDefault");

        builder.HasIndex(e => e.SortOrder)
            .HasDatabaseName("IX_SystemLanguages_SortOrder");

        // Properties
        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(10)
            .IsUnicode(false); // ASCII only for ISO codes

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.NativeName)
            .HasMaxLength(100);

        builder.Property(e => e.Description)
            .HasMaxLength(500);

        builder.Property(e => e.IconUrl)
            .HasMaxLength(500)
            .IsUnicode(false); // URLs are ASCII

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.IsDefault)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.SortOrder)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(e => e.UpdatedAt)
            .IsRequired(false);

        // Seed Data - Common Languages
        builder.HasData(
            new SystemLanguage
            {
                Id = new Guid("10000000-0000-0000-0000-000000000001"),
                Code = "en",
                Name = "English",
                NativeName = "English",
                Description = "English (United States)",
                IsActive = true,
                IsDefault = true,
                SortOrder = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new SystemLanguage
            {
                Id = new Guid("10000000-0000-0000-0000-000000000002"),
                Code = "es",
                Name = "Spanish",
                NativeName = "Español",
                Description = "Spanish (Spain)",
                IsActive = true,
                IsDefault = false,
                SortOrder = 2,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new SystemLanguage
            {
                Id = new Guid("10000000-0000-0000-0000-000000000003"),
                Code = "fr",
                Name = "French",
                NativeName = "Français",
                Description = "French (France)",
                IsActive = true,
                IsDefault = false,
                SortOrder = 3,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new SystemLanguage
            {
                Id = new Guid("10000000-0000-0000-0000-000000000004"),
                Code = "de",
                Name = "German",
                NativeName = "Deutsch",
                Description = "German (Germany)",
                IsActive = true,
                IsDefault = false,
                SortOrder = 4,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new SystemLanguage
            {
                Id = new Guid("10000000-0000-0000-0000-000000000005"),
                Code = "zh",
                Name = "Chinese",
                NativeName = "中文",
                Description = "Chinese (Simplified)",
                IsActive = false, // Inactive by default until translations ready
                IsDefault = false,
                SortOrder = 5,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
