using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenModular.Module.UAP.Migrations.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class uap_v0002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UAP_User");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UAP_Config");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UAP_User",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
