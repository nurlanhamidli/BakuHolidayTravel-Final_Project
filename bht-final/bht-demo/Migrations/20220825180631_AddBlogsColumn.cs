using Microsoft.EntityFrameworkCore.Migrations;

namespace bht_demo.Migrations
{
    public partial class AddBlogsColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Categories_CategoryId",
                table: "Tours");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Tours",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Tours",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Categories_CategoryId",
                table: "Tours",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Categories_CategoryId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Tours");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Tours",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Categories_CategoryId",
                table: "Tours",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
