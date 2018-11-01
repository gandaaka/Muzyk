using Microsoft.EntityFrameworkCore.Migrations;

namespace Muzyk.Migrations
{
    public partial class Removed_RIDFromUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Recommendations_RId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "RecommendationRId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RecommendationRId",
                table: "Users",
                column: "RecommendationRId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Recommendations_RecommendationRId",
                table: "Users",
                column: "RecommendationRId",
                principalTable: "Recommendations",
                principalColumn: "RId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Recommendations_RecommendationRId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RecommendationRId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RecommendationRId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "RId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RId",
                table: "Users",
                column: "RId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Recommendations_RId",
                table: "Users",
                column: "RId",
                principalTable: "Recommendations",
                principalColumn: "RId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
