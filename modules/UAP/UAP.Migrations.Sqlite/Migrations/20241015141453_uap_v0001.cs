using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenModular.Module.UAP.Migrations.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class uap_v0001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UAP_Account",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Tel = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    RealName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    NickName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Avatar = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Locked = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRealNameVerified = table.Column<bool>(type: "INTEGER", nullable: false),
                    ActivatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAP_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UAP_AccountDepartment",
                columns: table => new
                {
                    AccountId = table.Column<string>(type: "TEXT", nullable: false),
                    DepartmentId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UAP_AuthenticationRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Source = table.Column<string>(type: "TEXT", nullable: false),
                    Client = table.Column<string>(type: "TEXT", nullable: false),
                    IPv4 = table.Column<uint>(type: "INTEGER", nullable: true),
                    IPv6 = table.Column<string>(type: "TEXT", nullable: true),
                    Mac = table.Column<string>(type: "TEXT", nullable: true),
                    AccountId = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    AuthenticateTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAP_AuthenticationRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UAP_AuthenticationToken",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Client = table.Column<string>(type: "TEXT", nullable: false),
                    AccessToken = table.Column<string>(type: "TEXT", nullable: false),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: false),
                    Expires = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAP_AuthenticationToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UAP_Config",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ModuleCode = table.Column<string>(type: "TEXT", nullable: false),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAP_Config", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UAP_DataSeedingHistory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ModuleCode = table.Column<string>(type: "TEXT", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAP_DataSeedingHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UAP_Department",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    OrganizationId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ParentId = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAP_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UAP_Organization",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAP_Organization", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UAP_Account");

            migrationBuilder.DropTable(
                name: "UAP_AccountDepartment");

            migrationBuilder.DropTable(
                name: "UAP_AuthenticationRecord");

            migrationBuilder.DropTable(
                name: "UAP_AuthenticationToken");

            migrationBuilder.DropTable(
                name: "UAP_Config");

            migrationBuilder.DropTable(
                name: "UAP_DataSeedingHistory");

            migrationBuilder.DropTable(
                name: "UAP_Department");

            migrationBuilder.DropTable(
                name: "UAP_Organization");
        }
    }
}
