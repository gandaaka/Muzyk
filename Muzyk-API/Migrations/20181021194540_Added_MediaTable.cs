using Microsoft.EntityFrameworkCore.Migrations;

namespace Muzyk.Migrations
{
    public partial class Added_MediaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VideoUrl",
                table: "Videos",
                newName: "MediaUrl");

            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Photos",
                newName: "MediaUrl");

            migrationBuilder.AddColumn<string>(
                name: "MediaType",
                table: "Videos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaType",
                table: "Photos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "MediaUrl",
                table: "Videos",
                newName: "VideoUrl");

            migrationBuilder.RenameColumn(
                name: "MediaUrl",
                table: "Photos",
                newName: "PhotoUrl");
        }
    }
}
