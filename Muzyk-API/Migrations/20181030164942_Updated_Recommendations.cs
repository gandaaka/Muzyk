using Microsoft.EntityFrameworkCore.Migrations;

namespace Muzyk.Migrations
{
    public partial class Updated_Recommendations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Recommendations_RecommendationRId_RecommendationUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RecommendationRId_RecommendationUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RecommendationRId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RecommendationUserId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "RecommendedUser1",
                table: "Recommendations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedUser2",
                table: "Recommendations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedUser3",
                table: "Recommendations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedUser4",
                table: "Recommendations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedUser5",
                table: "Recommendations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecommendedUser1",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "RecommendedUser2",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "RecommendedUser3",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "RecommendedUser4",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "RecommendedUser5",
                table: "Recommendations");

            migrationBuilder.AddColumn<int>(
                name: "RecommendationRId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecommendationUserId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RecommendationRId_RecommendationUserId",
                table: "Users",
                columns: new[] { "RecommendationRId", "RecommendationUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Recommendations_RecommendationRId_RecommendationUserId",
                table: "Users",
                columns: new[] { "RecommendationRId", "RecommendationUserId" },
                principalTable: "Recommendations",
                principalColumns: new[] { "RId", "UserId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
