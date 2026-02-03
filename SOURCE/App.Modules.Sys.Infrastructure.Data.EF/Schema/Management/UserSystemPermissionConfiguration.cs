using App.Modules.Sys.Domain.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Data.EF.Schema.Management
{
    /// <summary>
    /// Fluent configuration for UserSystemPermission entity.
    /// </summary>
    public class UserSystemPermissionConfiguration : IEntityTypeConfiguration<UserSystemPermission>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UserSystemPermission> builder)
        {
            builder.ToTable("UserSystemPermissions", "sysmdl");

            // Primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Properties
            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.PermissionKey)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.GrantedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.GrantedBy)
                .IsRequired(false)
                .HasMaxLength(256);

            // Unique constraint: User can't have same permission twice
            builder.HasIndex(x => new { x.UserId, x.PermissionKey })
                .IsUnique()
                .HasDatabaseName("IX_UserSystemPermissions_UserId_PermissionKey");

            // Index for permission lookup
            builder.HasIndex(x => x.PermissionKey)
                .HasDatabaseName("IX_UserSystemPermissions_PermissionKey");

            // Relationships
            builder.HasOne(x => x.User)
                .WithMany(x => x.SystemPermissions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Permission)
                .WithMany(x => x.UserPermissions)
                .HasForeignKey(x => x.PermissionKey)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
