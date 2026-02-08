using App.Modules.Sys.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Data.EF.Domains.Identity;

/// <summary>
/// EF Core configuration for LocalUser entity.
/// </summary>
public class LocalUserConfiguration : IEntityTypeConfiguration<LocalUser>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<LocalUser> builder)
    {
        builder.ToTable("LocalUsers", "auth");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(e => e.NormalizedEmail)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(e => e.EmailConfirmed)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.IsEnabled)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.IsLockedOut)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.LockoutEndDateTimeUtc);

        builder.Property(e => e.AccessFailedCount)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(e => e.SecurityStamp)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.ConcurrencyStamp)
            .IsRequired()
            .HasMaxLength(100)
            .IsConcurrencyToken();

        builder.Property(e => e.CreatedOnDateTimeUtc).IsRequired();
        builder.Property(e => e.LastModifiedOnDateTimeUtc);
        builder.Property(e => e.LastLoginDateTimeUtc);

        builder.Property(e => e.TwoFactorEnabled)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.PhoneNumber).HasMaxLength(20);
        builder.Property(e => e.PhoneNumberConfirmed)
            .IsRequired()
            .HasDefaultValue(false);

        // Indexes
        builder.HasIndex(e => e.NormalizedEmail)
            .IsUnique()
            .HasDatabaseName("IX_LocalUsers_NormalizedEmail");

        builder.HasIndex(e => e.Email)
            .HasDatabaseName("IX_LocalUsers_Email");

        // Relationships
        builder.HasMany(e => e.Credentials)
            .WithOne(c => c.LocalUser)
            .HasForeignKey(c => c.LocalUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.ExternalLogins)
            .WithOne(l => l.LocalUser)
            .HasForeignKey(l => l.LocalUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.RefreshTokens)
            .WithOne(t => t.LocalUser)
            .HasForeignKey(t => t.LocalUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
