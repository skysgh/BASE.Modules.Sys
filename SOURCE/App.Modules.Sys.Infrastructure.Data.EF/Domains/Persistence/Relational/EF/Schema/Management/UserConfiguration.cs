using App.Modules.Sys.Domain.Domains.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Schema.Management
{
    /// <summary>
    /// Fluent configuration for User entity.
    /// Auto-discovered by DesignTimeModelBuilderOrchestrator at migration time.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "sysmdl");

            // Primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Properties
            builder.Property(x => x.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.LastLoginAt)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(x => x.IsActive)
                .HasDatabaseName("IX_Users_IsActive");

            builder.HasIndex(x => x.CreatedAt)
                .HasDatabaseName("IX_Users_CreatedAt");

            // Relationships
            builder.HasMany(x => x.Identities)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.SystemPermissions)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.IdentityLink)
                .WithOne(x => x.User)
                .HasForeignKey<IdentityLink>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
