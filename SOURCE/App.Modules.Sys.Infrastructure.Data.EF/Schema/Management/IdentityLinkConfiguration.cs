using App.Modules.Sys.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Data.EF.Schema.Management
{
    /// <summary>
    /// Fluent configuration for IdentityLink entity.
    /// </summary>
    public class IdentityLinkConfiguration : IEntityTypeConfiguration<IdentityLink>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<IdentityLink> builder)
        {
            builder.ToTable("IdentityLinks", "sysmdl");

            // Primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Properties
            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.PersonId)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.CreatedBy)
                .IsRequired(false)
                .HasMaxLength(256);

            // Unique constraint: One User per Person
            builder.HasIndex(x => x.UserId)
                .IsUnique()
                .HasDatabaseName("IX_IdentityLinks_UserId");

            builder.HasIndex(x => x.PersonId)
                .IsUnique()
                .HasDatabaseName("IX_IdentityLinks_PersonId");

            // Relationship to User (1:1)
            builder.HasOne(x => x.User)
                .WithOne(x => x.IdentityLink)
                .HasForeignKey<IdentityLink>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // NO navigation to Person - it's in another domain (opaque)
        }
    }
}
