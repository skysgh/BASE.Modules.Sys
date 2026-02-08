using App.Modules.Sys.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Data.EF.Domains.Identity;

/// <summary>
/// EF Core configuration for LocalUserCredential entity.
/// </summary>
public class LocalUserCredentialConfiguration : IEntityTypeConfiguration<LocalUserCredential>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<LocalUserCredential> builder)
    {
        builder.ToTable("LocalUserCredentials", "auth");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.LocalUserId).IsRequired();

        builder.Property(e => e.Type)
            .IsRequired()
            .HasConversion<int>();

        // The hash contains the embedded salt (PBKDF2 format)
        builder.Property(e => e.HashedValue)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.CreatedOnDateTimeUtc).IsRequired();
        builder.Property(e => e.ExpiresOnDateTimeUtc);

        builder.Property(e => e.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(e => e.RequireChangeOnNextLogin)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.LastUsedDateTimeUtc);

        builder.Property(e => e.Description).HasMaxLength(256);

        // Indexes
        builder.HasIndex(e => e.LocalUserId)
            .HasDatabaseName("IX_LocalUserCredentials_LocalUserId");

        builder.HasIndex(e => new { e.LocalUserId, e.Type, e.IsActive })
            .HasDatabaseName("IX_LocalUserCredentials_User_Type_Active");
    }
}

/// <summary>
/// EF Core configuration for LocalUserExternalLogin entity.
/// </summary>
public class LocalUserExternalLoginConfiguration : IEntityTypeConfiguration<LocalUserExternalLogin>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<LocalUserExternalLogin> builder)
    {
        builder.ToTable("LocalUserExternalLogins", "auth");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.LocalUserId).IsRequired();

        builder.Property(e => e.LoginProvider)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(e => e.ProviderKey)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(e => e.ProviderDisplayName).HasMaxLength(256);

        builder.Property(e => e.CreatedOnDateTimeUtc).IsRequired();
        builder.Property(e => e.LastUsedDateTimeUtc);

        // Unique constraint: one external login per provider per user
        builder.HasIndex(e => new { e.LoginProvider, e.ProviderKey })
            .IsUnique()
            .HasDatabaseName("IX_LocalUserExternalLogins_Provider_Key");

        builder.HasIndex(e => e.LocalUserId)
            .HasDatabaseName("IX_LocalUserExternalLogins_LocalUserId");
    }
}

/// <summary>
/// EF Core configuration for LocalUserRefreshToken entity.
/// </summary>
public class LocalUserRefreshTokenConfiguration : IEntityTypeConfiguration<LocalUserRefreshToken>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<LocalUserRefreshToken> builder)
    {
        builder.ToTable("LocalUserRefreshTokens", "auth");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.LocalUserId).IsRequired();

        builder.Property(e => e.TokenHandle)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.HashedToken)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(e => e.IssuedDateTimeUtc).IsRequired();
        builder.Property(e => e.ExpiresDateTimeUtc).IsRequired();
        builder.Property(e => e.ConsumedDateTimeUtc);

        builder.Property(e => e.IsRevoked)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(e => e.RevocationReason).HasMaxLength(256);

        builder.Property(e => e.FamilyId).IsRequired();
        builder.Property(e => e.ReplacedByTokenId);

        builder.Property(e => e.ClientId).HasMaxLength(100);
        builder.Property(e => e.DeviceId).HasMaxLength(256);
        builder.Property(e => e.IpAddress).HasMaxLength(45); // IPv6
        builder.Property(e => e.UserAgent).HasMaxLength(500);

        // Indexes
        builder.HasIndex(e => e.TokenHandle)
            .IsUnique()
            .HasDatabaseName("IX_LocalUserRefreshTokens_TokenHandle");

        builder.HasIndex(e => e.LocalUserId)
            .HasDatabaseName("IX_LocalUserRefreshTokens_LocalUserId");

        builder.HasIndex(e => e.FamilyId)
            .HasDatabaseName("IX_LocalUserRefreshTokens_FamilyId");

        builder.HasIndex(e => e.ExpiresDateTimeUtc)
            .HasDatabaseName("IX_LocalUserRefreshTokens_Expires");
    }
}
