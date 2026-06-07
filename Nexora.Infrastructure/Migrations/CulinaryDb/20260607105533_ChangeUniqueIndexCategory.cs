using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexora.Infrastructure.Migrations.CulinaryDb
{
    /// <inheritdoc />
    public partial class ChangeUniqueIndexCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_culinary_categories_core_companies_id_slug",
                table: "culinary_categories");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_categories_core_companies_id_parent_id_slug",
                table: "culinary_categories",
                columns: new[] { "core_companies_id", "parent_id", "slug" },
                unique: true,
                filter: "\"parent_id\" IS NOT NULL AND \"deleted_at\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_categories_core_companies_id_slug",
                table: "culinary_categories",
                columns: new[] { "core_companies_id", "slug" },
                unique: true,
                filter: "\"parent_id\" IS NULL AND \"deleted_at\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_culinary_categories_core_companies_id_parent_id_slug",
                table: "culinary_categories");

            migrationBuilder.DropIndex(
                name: "IX_culinary_categories_core_companies_id_slug",
                table: "culinary_categories");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_categories_core_companies_id_slug",
                table: "culinary_categories",
                columns: new[] { "core_companies_id", "slug" },
                unique: true,
                filter: "\"deleted_at\" IS NULL");
        }
    }
}
