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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAP_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UAP_AuthenticationRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Mode = table.Column<int>(type: "INTEGER", nullable: false),
                    Source = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Client = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IPv4 = table.Column<uint>(type: "INTEGER", nullable: true),
                    IPv6 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Mac = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: true),
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Client = table.Column<string>(type: "TEXT", nullable: false),
                    AccessToken = table.Column<string>(type: "TEXT", nullable: false),
                    RefreshToken = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModuleCode = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModuleCode = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Version = table.Column<int>(type: "INTEGER", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAP_DataSeedingHistory", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UAP_Account");

            migrationBuilder.DropTable(
                name: "UAP_AuthenticationRecord");

            migrationBuilder.DropTable(
                name: "UAP_AuthenticationToken");

            migrationBuilder.DropTable(
                name: "UAP_Config");

            migrationBuilder.DropTable(
                name: "UAP_DataSeedingHistory");
        }
    }
}
