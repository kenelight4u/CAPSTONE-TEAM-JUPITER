using Microsoft.EntityFrameworkCore.Migrations;

namespace JupiterCapstone.Migrations
{
    public partial class SubcategoryUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubCategoryImage",
                table: "SubCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategoryImage",
                table: "SubCategories");
        }
    }
}
