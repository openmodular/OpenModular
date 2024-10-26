using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OpenModular.Module.UAP.Migrations.Postgresql.Migrations
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAP_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UAP_AuthenticationRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mode = table.Column<int>(type: "integer", nullable: false),
                    Source = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Client = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IPv4 = table.Column<long>(type: "bigint", nullable: true),
                    IPv6 = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Mac = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: true),
                    AuthenticateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAP_AuthenticationRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UAP_AuthenticationToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Client = table.Column<string>(type: "text", nullable: false),
                    AccessToken = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Expires = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAP_AuthenticationToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UAP_Config",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModuleCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Key = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAP_Config", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UAP_DataSeedingHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModuleCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Version = table.Column<int>(type: "integer", maxLength: 50, nullable: false)
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
