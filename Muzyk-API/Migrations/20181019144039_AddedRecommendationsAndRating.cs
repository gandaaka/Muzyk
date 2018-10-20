using Microsoft.EntityFrameworkCore.Migrations;

namespace Muzyk.Migrations
{
    public partial class AddedRecommendationsAndRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecommendationRId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecommendationUserId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    Genre = table.Column<string>(nullable: false),
                    UserRating = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.UserId, x.Genre });
                });

            migrationBuilder.CreateTable(
                name: "Recommendations",
                columns: table => new
                {
                    RId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => new { x.RId, x.UserId });
                });

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

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Recommendations");

            migrationBuilder.DropIndex(
                name: "IX_Users_RecommendationRId_RecommendationUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RId",
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
