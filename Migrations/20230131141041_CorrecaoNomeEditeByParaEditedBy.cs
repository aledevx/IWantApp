using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IWantApp.Migrations
{
    public partial class CorrecaoNomeEditeByParaEditedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EditeBy",
                table: "Products",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "EditeBy",
                table: "Categories",
                newName: "EditedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Products",
                newName: "EditeBy");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "Categories",
                newName: "EditeBy");
        }
    }
}
