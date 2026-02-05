using App.Modules.Sys.Domain.Domains.Workspaces.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Modules.Sys.Infrastructure.Domains.Persistence.Relational.EF.Schema.Management;

/// <summary>
/// Fluent configuration for WorkspaceMember entity.
/// Auto-discovered via reflection by EF Core.
/// </summary>
public class WorkspaceMemberConfiguration : IEntityTypeConfiguration<WorkspaceMember>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<WorkspaceMember> builder)
    {
        builder.ToTable("WorkspaceMembers", "sysmdl");

        // Primary key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        // Foreign keys
        builder.Property(x => x.WorkspaceId)
            .IsRequired();

        builder.Property(x => x.UserId)
            .IsRequired();

        // Properties
        builder.Property(x => x.Role)
            .IsRequired()
            .HasMaxLength(50)
            .HasDefaultValue("Member");

        builder.Property(x => x.IsDefault)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.JoinedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        // Indexes
        builder.HasIndex(x => new { x.WorkspaceId, x.UserId })
            .IsUnique()
            .HasDatabaseName("IX_WorkspaceMembers_WorkspaceId_UserId");

        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("IX_WorkspaceMembers_UserId");

        builder.HasIndex(x => new { x.UserId, x.IsDefault })
            .HasDatabaseName("IX_WorkspaceMembers_UserId_IsDefault");

        // Relationships configured in WorkspaceConfiguration

        // Seed data - test users in workspaces
        SeedWorkspaceMembers(builder);
    }

    private static void SeedWorkspaceMembers(EntityTypeBuilder<WorkspaceMember> builder)
    {
        var now = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Test user GUIDs (these should match your User seed data when you create it)
        var adminUserId = new Guid("20000000-0000-0000-0000-000000000001");
        var user1Id = new Guid("20000000-0000-0000-0000-000000000002");
        var user2Id = new Guid("20000000-0000-0000-0000-000000000003");

        // Workspace GUIDs (from WorkspaceConfiguration)
        var defaultWorkspaceId = new Guid("10000000-0000-0000-0000-000000000001");
        var fooWorkspaceId = new Guid("10000000-0000-0000-0000-000000000002");
        var barWorkspaceId = new Guid("10000000-0000-0000-0000-000000000003");

        builder.HasData(
            // Admin user in all workspaces
            new WorkspaceMember
            {
                Id = new Guid("30000000-0000-0000-0000-000000000001"),
                WorkspaceId = defaultWorkspaceId,
                UserId = adminUserId,
                Role = "Owner",
                IsDefault = true,
                JoinedAt = now
            },
            new WorkspaceMember
            {
                Id = new Guid("30000000-0000-0000-0000-000000000002"),
                WorkspaceId = fooWorkspaceId,
                UserId = adminUserId,
                Role = "Admin",
                IsDefault = false,
                JoinedAt = now
            },
            new WorkspaceMember
            {
                Id = new Guid("30000000-0000-0000-0000-000000000003"),
                WorkspaceId = barWorkspaceId,
                UserId = adminUserId,
                Role = "Admin",
                IsDefault = false,
                JoinedAt = now
            },

            // User1 in Default and Foo
            new WorkspaceMember
            {
                Id = new Guid("30000000-0000-0000-0000-000000000004"),
                WorkspaceId = defaultWorkspaceId,
                UserId = user1Id,
                Role = "Member",
                IsDefault = true,
                JoinedAt = now
            },
            new WorkspaceMember
            {
                Id = new Guid("30000000-0000-0000-0000-000000000005"),
                WorkspaceId = fooWorkspaceId,
                UserId = user1Id,
                Role = "Member",
                IsDefault = false,
                JoinedAt = now
            },

            // User2 in Foo and Bar
            new WorkspaceMember
            {
                Id = new Guid("30000000-0000-0000-0000-000000000006"),
                WorkspaceId = fooWorkspaceId,
                UserId = user2Id,
                Role = "Member",
                IsDefault = true,
                JoinedAt = now
            },
            new WorkspaceMember
            {
                Id = new Guid("30000000-0000-0000-0000-000000000007"),
                WorkspaceId = barWorkspaceId,
                UserId = user2Id,
                Role = "Guest",
                IsDefault = false,
                JoinedAt = now
            }
        );
    }
}
