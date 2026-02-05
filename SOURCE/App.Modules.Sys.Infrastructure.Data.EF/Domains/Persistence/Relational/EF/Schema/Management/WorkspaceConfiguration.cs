using App.Modules.Sys.Domain.Domains.Workspaces.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Schema.Management;

/// <summary>
/// Fluent configuration for Workspace entity.
/// Auto-discovered via reflection by EF Core.
/// </summary>
public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.ToTable("Workspaces", "sysmdl");

        // Primary key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        // Properties
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.OrganizationName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.LogoUrl)
            .HasMaxLength(500);

        builder.Property(x => x.PrimaryColor)
            .HasMaxLength(7); // #RRGGBB

        builder.Property(x => x.CustomCssUrl)
            .HasMaxLength(500);

        builder.Property(x => x.SubscriptionTier)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValue("Free");

        builder.Property(x => x.MaxUsers)
            .IsRequired(false);

        builder.Property(x => x.StorageLimitGb)
            .IsRequired(false);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.Property(x => x.SettingsJson)
            .HasColumnType("nvarchar(max)");

        // Indexes
        builder.HasIndex(x => x.Slug)
            .IsUnique()
            .HasDatabaseName("IX_Workspaces_Slug");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("IX_Workspaces_IsActive");

        builder.HasIndex(x => x.OrganizationName)
            .HasDatabaseName("IX_Workspaces_OrganizationName");

        // Relationships
        builder.HasMany(x => x.Members)
            .WithOne(x => x.Workspace)
            .HasForeignKey(x => x.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed data - default workspaces for development/testing
        SeedWorkspaces(builder);
    }

    private static void SeedWorkspaces(EntityTypeBuilder<Workspace> builder)
    {
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc); // Fixed date for seed data

        builder.HasData(
            // Default workspace
            new Workspace
            {
                Id = new Guid("10000000-0000-0000-0000-000000000001"),
                Title = "Default Workspace",
                Description = "System default workspace for initial setup",
                Slug = "default",
                OrganizationName = "Default Organization",
                PrimaryColor = "#0078D4",
                SubscriptionTier = "Free",
                MaxUsers = 10,
                StorageLimitGb = 5,
                IsActive = true,
                CreatedAt = now,
                UpdatedAt = now
            },
            // Foo workspace
            new Workspace
            {
                Id = new Guid("10000000-0000-0000-0000-000000000002"),
                Title = "Foo Workspace",
                Description = "Test workspace for Foo team",
                Slug = "foo",
                OrganizationName = "Foo Corporation",
                LogoUrl = "https://via.placeholder.com/150/FF6B6B/FFFFFF?text=FOO",
                PrimaryColor = "#FF6B6B",
                SubscriptionTier = "Pro",
                MaxUsers = 50,
                StorageLimitGb = 100,
                IsActive = true,
                CreatedAt = now,
                UpdatedAt = now
            },
            // Bar workspace
            new Workspace
            {
                Id = new Guid("10000000-0000-0000-0000-000000000003"),
                Title = "Bar Workspace",
                Description = "Test workspace for Bar team",
                Slug = "bar",
                OrganizationName = "Bar Industries",
                LogoUrl = "https://via.placeholder.com/150/4ECDC4/FFFFFF?text=BAR",
                PrimaryColor = "#4ECDC4",
                CustomCssUrl = "https://example.com/bar-theme.css",
                SubscriptionTier = "Enterprise",
                MaxUsers = null, // Unlimited
                StorageLimitGb = null, // Unlimited
                IsActive = true,
                CreatedAt = now,
                UpdatedAt = now
            }
        );
    }
}
