using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable

namespace Nexora.Infrastructure.Migrations.CulinaryDb
{
    /// <inheritdoc />
    public partial class CreateCulinaryRecipeNameTrgm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                CREATE INDEX IF NOT EXISTS idx_culinary_recipes_name_trgm
                ON culinary_recipes
                USING gin (name gin_trgm_ops);
                """
            );

            migrationBuilder.Sql(
                """
                CREATE INDEX IF NOT EXISTS idx_culinary_recipes_short_description_trgm
                ON culinary_recipes
                USING gin (short_description gin_trgm_ops);
                """
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "DROP INDEX IF EXISTS idx_culinary_recipes_name_trgm;"
            );

            migrationBuilder.Sql(
                "DROP INDEX IF EXISTS idx_culinary_recipes_short_description_trgm;"
            );
        }
    }
}