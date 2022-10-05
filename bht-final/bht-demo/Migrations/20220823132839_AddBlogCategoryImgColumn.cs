using Microsoft.EntityFrameworkCore.Migrations;

namespace bht_demo.Migrations
{
    public partial class AddBlogCategoryImgColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "BlogCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "BlogCategories");
        }
    }
}
