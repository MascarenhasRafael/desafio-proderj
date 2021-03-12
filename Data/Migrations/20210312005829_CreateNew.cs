using Microsoft.EntityFrameworkCore.Migrations;

namespace desafio_proderj.Data.Migrations
{
    public partial class CreateNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "New",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Content = table.Column<string>(maxLength: 500)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_New", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "New");
        }
    }
}
