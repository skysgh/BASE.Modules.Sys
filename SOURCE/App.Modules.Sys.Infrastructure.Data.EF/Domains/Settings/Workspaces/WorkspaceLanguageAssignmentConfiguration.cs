using App.Modules.Sys.Domain.ReferenceData;
using App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Domains.Settings.Workspaces;

/// <summary>
/// EF Core configuration for WorkspaceLanguageAssignment entity.
/// Maps workspace-specific language enablements.
/// 
/// Note: This is a simple join table. The actual default language for a workspace
/// should be stored in Settings with key "Language.Default" at the Workspace level.
/// </summary>
public sealed class WorkspaceLanguageAssignmentConfiguration : IEntityTypeConfiguration<WorkspaceLanguageAssignment>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<WorkspaceLanguageAssignment> builder)
    {
        int order = 1;

        // ============================================================
        // 1. Table & Primary Key
        // ============================================================
        builder.ToTable(
            DbSchemaTableNameConstants.WorkspaceLanguageAssignments,
            DbSchemaSchemaNameConstants.ReferenceData);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasColumnOrder(order++)
            .IsRequired()
            .ValueGeneratedNever();

        // ============================================================
        // 2. FK Properties & Indexes
        // ============================================================
        builder.Property(e => e.WorkspaceId)
            .HasColumnOrder(order++)
            .IsRequired();

        builder.HasIndex(e => e.WorkspaceId)
            .HasDatabaseName("IX_WorkspaceLanguageAssignments_WorkspaceId");

        builder.Property(e => e.LanguageCode)
            .HasColumnOrder(order++)
            .IsRequired()
            .HasMaxLength(10)
            .IsUnicode(false); // ASCII for ISO language codes

        // Composite unique constraint: one language per workspace
        builder.HasIndex(e => new { e.WorkspaceId, e.LanguageCode })
            .HasDatabaseName("IX_WorkspaceLanguageAssignments_Workspace_Language")
            .IsUnique();

        // ============================================================
        // 3. Custom Properties
        // ============================================================
        builder.Property(e => e.IsDefault)
            .HasColumnOrder(order++)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.SortOrder)
            .HasColumnOrder(order++)
            .IsRequired()
            .HasDefaultValue(0);

        // ============================================================
        // 4. Audit timestamps (simple - no full auditability)
        // ============================================================
        builder.Property(e => e.CreatedAt)
            .HasColumnOrder(order++)
            .IsRequired();

        // ============================================================
        // 5. Navigation Properties (none - use repository joins)
        // ============================================================
        // No navigation properties to avoid circular dependencies.
        // Use repository joins when workspace/language details are needed.
    }
}
