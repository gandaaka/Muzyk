using Microsoft.EntityFrameworkCore.Migrations;

namespace Muzyk.Migrations
{
    public partial class Updated_RecommendationsTable_UpdatedColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecommendedUser5",
                table: "Recommendations",
                newName: "RelatedUser5");

            migrationBuilder.RenameColumn(
                name: "RecommendedUser4",
                table: "Recommendations",
                newName: "RelatedUser4");

            migrationBuilder.RenameColumn(
                name: "RecommendedUser3",
                table: "Recommendations",
                newName: "RelatedUser3");

            migrationBuilder.RenameColumn(
                name: "RecommendedUser2",
                table: "Recommendations",
                newName: "RelatedUser2");

            migrationBuilder.RenameColumn(
                name: "RecommendedUser1",
                table: "Recommendations",
                newName: "RelatedUser1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RelatedUser5",
                table: "Recommendations",
                newName: "RecommendedUser5");

            migrationBuilder.RenameColumn(
                name: "RelatedUser4",
                table: "Recommendations",
                newName: "RecommendedUser4");

            migrationBuilder.RenameColumn(
                name: "RelatedUser3",
                table: "Recommendations",
                newName: "RecommendedUser3");

            migrationBuilder.RenameColumn(
                name: "RelatedUser2",
                table: "Recommendations",
                newName: "RecommendedUser2");

            migrationBuilder.RenameColumn(
                name: "RelatedUser1",
                table: "Recommendations",
                newName: "RecommendedUser1");
        }
    }
}
