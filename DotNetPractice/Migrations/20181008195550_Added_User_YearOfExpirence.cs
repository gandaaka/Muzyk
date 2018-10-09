using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetPractice.Migrations
{
    public partial class Added_User_YearOfExpirence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "Users");
        }
    }
}
