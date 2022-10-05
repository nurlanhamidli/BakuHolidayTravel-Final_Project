using Microsoft.EntityFrameworkCore.Migrations;

namespace bht_demo.Migrations
{
    public partial class SettingsTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderLogo = table.Column<string>(maxLength: 150, nullable: true),
                    FooterLogo = table.Column<string>(maxLength: 150, nullable: true),
                    Tel1 = table.Column<string>(maxLength: 50, nullable: true),
                    Tel2 = table.Column<string>(maxLength: 50, nullable: true),
                    TelWp = table.Column<string>(maxLength: 50, nullable: true),
                    SkypeUrl = table.Column<string>(maxLength: 100, nullable: true),
                    MessengerUrl = table.Column<string>(maxLength: 100, nullable: true),
                    FacebookUrl = table.Column<string>(maxLength: 150, nullable: true),
                    InstagramUrl = table.Column<string>(maxLength: 150, nullable: true),
                    LinkedinUrl = table.Column<string>(maxLength: 150, nullable: true),
                    TwitterUrl = table.Column<string>(maxLength: 150, nullable: true),
                    YoutubeUrl = table.Column<string>(maxLength: 150, nullable: true),
                    Adress = table.Column<string>(maxLength: 150, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
