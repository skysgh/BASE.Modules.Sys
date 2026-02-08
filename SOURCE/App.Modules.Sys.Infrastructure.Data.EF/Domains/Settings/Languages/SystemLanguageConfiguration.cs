using App.Modules.Sys.Domain.ReferenceData;
using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Domains.Settings.Language;

/// <summary>
/// EF Core configuration for SystemLanguage entity.
/// 
/// Uses contract-based EntityTypeBuilderExtensions for DRY, 
/// compile-time safe configuration.
/// 
/// Pattern: Table -> PK -> Contracts (in order of column preference) -> Custom
/// 
/// SystemLanguage implements:
/// - IHasGuidId: Primary key
/// - IHasKey: ISO code as business key
/// - IHasEnabled: Enable/disable
/// - IHasDisplayHints: Sort order and style hints
/// - IHasTitleAndDescription: Display name and description
/// - IHasImageUrlNullable: Optional flag/icon
/// 
/// Note: Seed data is in Seeding/ReferenceData/SystemLanguageSeeder.cs
/// </summary>
public sealed class SystemLanguagesConfiguration : IEntityTypeConfiguration<SystemLanguage>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<SystemLanguage> builder)
    {
        int order = 0;

        // ============================================================
        // 1. Table & Schema
        // ============================================================
        builder.DefineTable(
            DbSchemaTableNameConstants.SystemLanguages, 
            DbSchemaSchemaNameConstants.ReferenceData);

        // ============================================================
        // 2. Primary Key (from IHasGuidId contract)
        // ============================================================
        builder.DefineIHasGuidId(ref order);

        // ============================================================
        // 3. Contract-Based Properties (in column order)
        // ============================================================
        
        // IHasKey - ISO language code as unique business key
        builder.DefineIHasKey(ref order);

        // IHasEnabled - whether language is active
        builder.DefineIHasEnabled(ref order);

        // IHasTitleAndDescription - display name and description
        builder.DefineIHasTitleAndDescription(ref order);

        // IHasImageUrlNullable - optional flag icon
        builder.DefineString(
            x => x.ImageUrl, 
            ref order, 
            isRequired: false, 
            maxLength: DbSchemaFieldSizeConstants.UrlLength, 
            unicode: false);

        // IHasDisplayHints - sort order and style hints
        builder.DefineIHasDisplayHints(ref order);

        // ============================================================
        // 4. Custom Properties (entity-specific, not in contracts)
        // ============================================================
        
        // NativeName - native language name (e.g., "Espanol")
        builder.DefineString(
            x => x.NativeName, 
            ref order, 
            isRequired: false, 
            maxLength: DbSchemaFieldSizeConstants.TitleLength, 
            unicode: true);

        // IsDefault - only one language should be default
        builder.DefineBool(
            x => x.IsDefault, 
            ref order, 
            isRequired: true, 
            defaultValue: false);

        // ============================================================
        // 5. Indexes
        // ============================================================
        builder.HasIndex(e => e.IsDefault)
            .HasDatabaseName($"IX_{DbSchemaTableNameConstants.SystemLanguages}_IsDefault")
            .HasFilter("[IsDefault] = 1")
            .IsUnique();
    }
}
