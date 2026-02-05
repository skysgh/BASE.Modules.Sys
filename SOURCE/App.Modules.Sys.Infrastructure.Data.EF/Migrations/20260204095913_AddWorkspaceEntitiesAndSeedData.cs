using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
#pragma warning disable CA1861 // Prefer static readonly fields over constant array arguments

namespace App.Modules.Sys.Infrastructure.Data.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkspaceEntitiesAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workspaces",
                schema: "sysmdl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PrimaryColor = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    CustomCssUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SubscriptionTier = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Free"),
                    MaxUsers = table.Column<int>(type: "int", nullable: true),
                    StorageLimitGb = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SettingsJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkspaceMembers",
                schema: "sysmdl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Member"),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspaceMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkspaceMembers_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalSchema: "sysmdl",
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "sysmdl",
                table: "Workspaces",
                columns: new[] { "Id", "CreatedAt", "CustomCssUrl", "Description", "IsActive", "LogoUrl", "MaxUsers", "OrganizationName", "PrimaryColor", "SettingsJson", "Slug", "StorageLimitGb", "SubscriptionTier", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "System default workspace for initial setup", true, null, 10, "Default Organization", "#0078D4", null, "default", 5, "Free", "Default Workspace", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Test workspace for Foo team", true, "https://via.placeholder.com/150/FF6B6B/FFFFFF?text=FOO", 50, "Foo Corporation", "#FF6B6B", null, "foo", 100, "Pro", "Foo Workspace", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("10000000-0000-0000-0000-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://example.com/bar-theme.css", "Test workspace for Bar team", true, "https://via.placeholder.com/150/4ECDC4/FFFFFF?text=BAR", null, "Bar Industries", "#4ECDC4", null, "bar", null, "Enterprise", "Bar Workspace", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "sysmdl",
                table: "WorkspaceMembers",
                columns: new[] { "Id", "IsDefault", "JoinedAt", "Role", "UserId", "WorkspaceId" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000001"), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Owner", new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000001") });

            migrationBuilder.InsertData(
                schema: "sysmdl",
                table: "WorkspaceMembers",
                columns: new[] { "Id", "JoinedAt", "Role", "UserId", "WorkspaceId" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Admin", new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000002") },
                    { new Guid("30000000-0000-0000-0000-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Admin", new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000003") }
                });

            migrationBuilder.InsertData(
                schema: "sysmdl",
                table: "WorkspaceMembers",
                columns: new[] { "Id", "IsDefault", "JoinedAt", "Role", "UserId", "WorkspaceId" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000004"), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Member", new Guid("20000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000001") });

            migrationBuilder.InsertData(
                schema: "sysmdl",
                table: "WorkspaceMembers",
                columns: new[] { "Id", "JoinedAt", "Role", "UserId", "WorkspaceId" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000005"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Member", new Guid("20000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000002") });

            migrationBuilder.InsertData(
                schema: "sysmdl",
                table: "WorkspaceMembers",
                columns: new[] { "Id", "IsDefault", "JoinedAt", "Role", "UserId", "WorkspaceId" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000006"), true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Member", new Guid("20000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000002") });

            migrationBuilder.InsertData(
                schema: "sysmdl",
                table: "WorkspaceMembers",
                columns: new[] { "Id", "JoinedAt", "Role", "UserId", "WorkspaceId" },
                values: new object[] { new Guid("30000000-0000-0000-0000-000000000007"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Guest", new Guid("20000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000003") });

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceMembers_UserId",
                schema: "sysmdl",
                table: "WorkspaceMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceMembers_UserId_IsDefault",
                schema: "sysmdl",
                table: "WorkspaceMembers",
                columns: new[] { "UserId", "IsDefault" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceMembers_WorkspaceId_UserId",
                schema: "sysmdl",
                table: "WorkspaceMembers",
                columns: new[] { "WorkspaceId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_IsActive",
                schema: "sysmdl",
                table: "Workspaces",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_OrganizationName",
                schema: "sysmdl",
                table: "Workspaces",
                column: "OrganizationName");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_Slug",
                schema: "sysmdl",
                table: "Workspaces",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkspaceMembers",
                schema: "sysmdl");

            migrationBuilder.DropTable(
                name: "Workspaces",
                schema: "sysmdl");
        }
    }
}
