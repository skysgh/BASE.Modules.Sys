using App.Modules.Sys.Domain.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Data.EF.Schema.Management
{
    /// <summary>
    /// Fluent configuration for SessionOperation entity.
    /// Auto-discovered by DesignTimeModelBuilderOrchestrator.
    /// </summary>
    public class SessionOperationConfiguration : IEntityTypeConfiguration<SessionOperation>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<SessionOperation> builder)
        {
            builder.ToTable("SessionOperations", "sysmdl");

            // Primary key
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // Properties
            builder.Property(x => x.SessionId)
                .IsRequired();

            builder.Property(x => x.OperationType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Resource)
                .IsRequired()
                .HasMaxLength(2000);  // URLs can be long

            builder.Property(x => x.HttpMethod)
                .IsRequired(false)
                .HasMaxLength(10);

            builder.Property(x => x.IpAddress)
                .IsRequired(false)
                .HasMaxLength(45);  // IPv6 max length

            builder.Property(x => x.UserAgent)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(x => x.Timestamp)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.StatusCode)
                .IsRequired(false);

            builder.Property(x => x.DurationMs)
                .IsRequired(false);

            builder.Property(x => x.IsSuccess)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(x => x.ErrorMessage)
                .IsRequired(false)
                .HasMaxLength(2000);

            builder.Property(x => x.RequestSize)
                .IsRequired(false);

            builder.Property(x => x.ResponseSize)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(x => x.SessionId)
                .HasDatabaseName("IX_SessionOperations_SessionId");

            builder.HasIndex(x => x.Timestamp)
                .HasDatabaseName("IX_SessionOperations_Timestamp");

            builder.HasIndex(x => x.OperationType)
                .HasDatabaseName("IX_SessionOperations_OperationType");

            builder.HasIndex(x => x.IsSuccess)
                .HasDatabaseName("IX_SessionOperations_IsSuccess");

            builder.HasIndex(x => new { x.SessionId, x.Timestamp })
                .HasDatabaseName("IX_SessionOperations_Session_Timestamp");

            // Relationship to Session
            builder.HasOne(x => x.Session)
                .WithMany(x => x.Operations)
                .HasForeignKey(x => x.SessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
