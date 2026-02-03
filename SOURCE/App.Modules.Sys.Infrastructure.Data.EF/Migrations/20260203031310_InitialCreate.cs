using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Modules.Sys.Infrastructure.Data.EF.Migrations
{
    /// <inheritdoc />
    [SuppressMessage("Performance", "CA1861:Prefer static readonly fields over constant array arguments", 
        Justification = "Generated migration code")]
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sysmdl");

            migrationBuilder.CreateTable(
                name: "Settings",
                schema: "sysmdl",
                columns: table => new
                {
                    WorkspaceId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Key = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SerializedTypeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SerializedTypeValue = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => new { x.WorkspaceId, x.UserId, x.Key });
                });

            migrationBuilder.CreateTable(
                name: "SystemPermissions",
                schema: "sysmdl",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "System"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemPermissions", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "sysmdl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityLinks",
                schema: "sysmdl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityLinks_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "sysmdl",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                schema: "sysmdl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SessionToken = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastActivityAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TerminatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "sysmdl",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserIdentities",
                schema: "sysmdl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProviderUserId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastUsedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIdentities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserIdentities_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "sysmdl",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSystemPermissions",
                schema: "sysmdl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionKey = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GrantedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    GrantedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSystemPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSystemPermissions_SystemPermissions_PermissionKey",
                        column: x => x.PermissionKey,
                        principalSchema: "sysmdl",
                        principalTable: "SystemPermissions",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSystemPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "sysmdl",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionOperations",
                schema: "sysmdl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Resource = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    HttpMethod = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    StatusCode = table.Column<int>(type: "int", nullable: true),
                    DurationMs = table.Column<long>(type: "bigint", nullable: true),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    RequestSize = table.Column<long>(type: "bigint", nullable: true),
                    ResponseSize = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionOperations_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalSchema: "sysmdl",
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityLinks_PersonId",
                schema: "sysmdl",
                table: "IdentityLinks",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityLinks_UserId",
                schema: "sysmdl",
                table: "IdentityLinks",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionOperations_IsSuccess",
                schema: "sysmdl",
                table: "SessionOperations",
                column: "IsSuccess");

            migrationBuilder.CreateIndex(
                name: "IX_SessionOperations_OperationType",
                schema: "sysmdl",
                table: "SessionOperations",
                column: "OperationType");

            migrationBuilder.CreateIndex(
                name: "IX_SessionOperations_Session_Timestamp",
                schema: "sysmdl",
                table: "SessionOperations",
                columns: new[] { "SessionId", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_SessionOperations_SessionId",
                schema: "sysmdl",
                table: "SessionOperations",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionOperations_Timestamp",
                schema: "sysmdl",
                table: "SessionOperations",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_CreatedAt",
                schema: "sysmdl",
                table: "Sessions",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ExpiresAt",
                schema: "sysmdl",
                table: "Sessions",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionToken",
                schema: "sysmdl",
                table: "Sessions",
                column: "SessionToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                schema: "sysmdl",
                table: "Sessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_IsLocked",
                schema: "sysmdl",
                table: "Settings",
                column: "IsLocked");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_Key",
                schema: "sysmdl",
                table: "Settings",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_WorkspaceId_Key",
                schema: "sysmdl",
                table: "Settings",
                columns: new[] { "WorkspaceId", "Key" });

            migrationBuilder.CreateIndex(
                name: "IX_SystemPermissions_Category",
                schema: "sysmdl",
                table: "SystemPermissions",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentities_Email",
                schema: "sysmdl",
                table: "UserIdentities",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentities_Provider_ProviderUserId",
                schema: "sysmdl",
                table: "UserIdentities",
                columns: new[] { "Provider", "ProviderUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserIdentities_UserId",
                schema: "sysmdl",
                table: "UserIdentities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedAt",
                schema: "sysmdl",
                table: "Users",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsActive",
                schema: "sysmdl",
                table: "Users",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_UserSystemPermissions_PermissionKey",
                schema: "sysmdl",
                table: "UserSystemPermissions",
                column: "PermissionKey");

            migrationBuilder.CreateIndex(
                name: "IX_UserSystemPermissions_UserId_PermissionKey",
                schema: "sysmdl",
                table: "UserSystemPermissions",
                columns: new[] { "UserId", "PermissionKey" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityLinks",
                schema: "sysmdl");

            migrationBuilder.DropTable(
                name: "SessionOperations",
                schema: "sysmdl");

            migrationBuilder.DropTable(
                name: "Settings",
                schema: "sysmdl");

            migrationBuilder.DropTable(
                name: "UserIdentities",
                schema: "sysmdl");

            migrationBuilder.DropTable(
                name: "UserSystemPermissions",
                schema: "sysmdl");

            migrationBuilder.DropTable(
                name: "Sessions",
                schema: "sysmdl");

            migrationBuilder.DropTable(
                name: "SystemPermissions",
                schema: "sysmdl");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "sysmdl");
        }
    }
}
