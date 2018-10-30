using Microsoft.EntityFrameworkCore.Migrations;

namespace Muzyk.Migrations
{
    public partial class Updated_RecommendationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
