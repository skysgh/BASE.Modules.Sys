using App.Modules.Sys.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Data.EF.Schema.Management
{
    /// <summary>
    /// Fluent configuration for UserIdentity entity.
    /// </summary>
    public class UserIdentityConfiguration : IEntityTypeConfiguration<UserIdentity>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<UserIdentity> builder)
        {
            builder.ToTable("UserIdentities", "sysmdl");

            // Primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Properties
            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Provider)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.ProviderUserId)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.Email)
                .IsRequired(false)
                .HasMaxLength(256);

            builder.Property(x => x.PasswordHash)
                .IsRequired(false)
                .HasMaxLength(500);  // Argon2/bcrypt output

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.LastUsedAt)
                .IsRequired(false);

            // Unique constraint: One provider per external user ID
            builder.HasIndex(x => new { x.Provider, x.ProviderUserId })
                .IsUnique()
                .HasDatabaseName("IX_UserIdentities_Provider_ProviderUserId");

            builder.HasIndex(x => x.Email)
                .HasDatabaseName("IX_UserIdentities_Email");

            // Index for lookup by user
            builder.HasIndex(x => x.UserId)
                .HasDatabaseName("IX_UserIdentities_UserId");

            // Relationship to User
            builder.HasOne(x => x.User)
                .WithMany(x => x.Identities)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
