using App.Modules.Sys.Domain.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Data.EF.Schema.Management
{
    /// <summary>
    /// Fluent configuration for SystemPermission entity.
    /// </summary>
    public class SystemPermissionConfiguration : IEntityTypeConfiguration<SystemPermission>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<SystemPermission> builder)
        {
            builder.ToTable("SystemPermissions", "sysmdl");

            // Primary key (string key)
            builder.HasKey(x => x.Key);
            builder.Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(200);

            // Properties
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Category)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValue("System");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            builder.HasIndex(x => x.Category)
                .HasDatabaseName("IX_SystemPermissions_Category");

            // Relationships
            builder.HasMany(x => x.UserPermissions)
                .WithOne(x => x.Permission)
                .HasForeignKey(x => x.PermissionKey)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
