using App.Modules.Sys.Domain.Domains.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Schema.Management
{
    /// <summary>
    /// Fluent configuration for SettingValue entity.
    /// </summary>
    public class SettingValueConfiguration : IEntityTypeConfiguration<SettingValue>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<SettingValue> builder)
        {
            builder.ToTable("Settings", "sysmdl");

            // Composite primary key (WorkspaceId + UserId + Key)
            builder.HasKey(x => new { x.WorkspaceId, x.UserId, x.Key });

            // Properties
            builder.Property(x => x.WorkspaceId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.SerializedTypeName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.SerializedTypeValue)
                .IsRequired(false)  // Nullable via interface
                .HasMaxLength(4000);

            builder.Property(x => x.IsLocked)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.LastModified)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.ModifiedBy)
                .IsRequired(false)
                .HasMaxLength(256);

            // Indexes for hierarchy queries
            builder.HasIndex(x => x.Key)
                .HasDatabaseName("IX_Settings_Key");

            builder.HasIndex(x => new { x.WorkspaceId, x.Key })
                .HasDatabaseName("IX_Settings_WorkspaceId_Key");

            builder.HasIndex(x => x.IsLocked)
                .HasDatabaseName("IX_Settings_IsLocked");
        }
    }
}
