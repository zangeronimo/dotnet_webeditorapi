using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexora.Infrastructure.Migrations.CulinaryDb
{
    /// <inheritdoc />
    public partial class AddAverageAndTotalRatingsToRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "average_rating",
                table: "culinary_recipes",
                type: "numeric(3,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "total_ratings",
                table: "culinary_recipes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_culinary_recipes_core_companies_id_average_rating",
                table: "culinary_recipes",
                columns: new[] { "core_companies_id", "average_rating" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_culinary_recipes_core_companies_id_average_rating",
                table: "culinary_recipes");

            migrationBuilder.DropColumn(
                name: "average_rating",
                table: "culinary_recipes");

            migrationBuilder.DropColumn(
                name: "total_ratings",
                table: "culinary_recipes");
        }
    }
}
