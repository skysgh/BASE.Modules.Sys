using App.Modules.Sys.Domain.Domains.Sessions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Schema.Management
{
    /// <summary>
    /// Fluent configuration for Session entity.
    /// Auto-discovered by DesignTimeModelBuilderOrchestrator.
    /// </summary>
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions", "sysmdl");

            // Primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Properties
            builder.Property(x => x.UserId)
                .IsRequired(false);  // Nullable = anonymous session

            builder.Property(x => x.SessionToken)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.LastActivityAt)
                .IsRequired(false);

            builder.Property(x => x.ExpiresAt)
                .IsRequired(false);

            builder.Property(x => x.TerminatedAt)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(x => x.SessionToken)
                .IsUnique()
                .HasDatabaseName("IX_Sessions_SessionToken");

            builder.HasIndex(x => x.UserId)
                .HasDatabaseName("IX_Sessions_UserId");

            builder.HasIndex(x => x.CreatedAt)
                .HasDatabaseName("IX_Sessions_CreatedAt");

            builder.HasIndex(x => x.ExpiresAt)
                .HasDatabaseName("IX_Sessions_ExpiresAt");

            builder.HasIndex(x => x.IsActive)
                .HasDatabaseName("IX_Sessions_IsActive")
                .HasFilter("[TerminatedAt] IS NULL AND ([ExpiresAt] IS NULL OR [ExpiresAt] > GETUTCDATE())");

            // Relationships
            builder.HasOne(x => x.User)
                .WithMany()  // User doesn't need Sessions collection
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull);  // Keep session if user deleted

            builder.HasMany(x => x.Operations)
                .WithOne(x => x.Session)
                .HasForeignKey(x => x.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ignore computed properties
            builder.Ignore(x => x.IsAuthenticated);
            builder.Ignore(x => x.IsExpired);
            builder.Ignore(x => x.IsActive);
        }
    }
}
