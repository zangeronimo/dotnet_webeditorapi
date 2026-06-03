using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexora.Infrastructure.Migrations.CulinaryDb
{
    /// <inheritdoc />
    public partial class CreateCulinaryRecipeRatings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "culinary_recipe_ratings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    score = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    published_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    culinary_recipes_id = table.Column<Guid>(type: "uuid", nullable: false),
                    core_companies_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_culinary_recipe_ratings", x => x.id);
                    table.ForeignKey(
                        name: "FK_culinary_recipe_ratings_culinary_recipes_culinary_recipes_id",
                        column: x => x.culinary_recipes_id,
                        principalTable: "culinary_recipes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_culinary_recipe_ratings_core_companies_id",
                table: "culinary_recipe_ratings",
                column: "core_companies_id");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_recipe_ratings_core_companies_id_culinary_recipes_~",
                table: "culinary_recipe_ratings",
                columns: new[] { "core_companies_id", "culinary_recipes_id", "status" });

            migrationBuilder.CreateIndex(
                name: "IX_culinary_recipe_ratings_core_companies_id_status",
                table: "culinary_recipe_ratings",
                columns: new[] { "core_companies_id", "status" });

            migrationBuilder.CreateIndex(
                name: "IX_culinary_recipe_ratings_culinary_recipes_id",
                table: "culinary_recipe_ratings",
                column: "culinary_recipes_id");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_recipe_ratings_deleted_at",
                table: "culinary_recipe_ratings",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_recipe_ratings_published_at",
                table: "culinary_recipe_ratings",
                column: "published_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "culinary_recipe_ratings");
        }
    }
}
