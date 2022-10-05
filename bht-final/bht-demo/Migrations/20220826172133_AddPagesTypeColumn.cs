using Microsoft.EntityFrameworkCore.Migrations;

namespace bht_demo.Migrations
{
    public partial class AddPagesTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ServiceAndAbouts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ServiceAndAbouts");
        }
    }
}
