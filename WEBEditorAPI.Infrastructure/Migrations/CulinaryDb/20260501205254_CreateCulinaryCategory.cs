using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBEditorAPI.Infrastructure.Migrations.CulinaryDb
{
    /// <inheritdoc />
    public partial class CreateCulinaryCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "recipe_categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    slug = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    name = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    meta_title = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    meta_description = table.Column<string>(type: "character varying(155)", maxLength: 155, nullable: true),
                    core_companies_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_recipe_categories_recipe_categories_parent_id",
                        column: x => x.parent_id,
                        principalTable: "recipe_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_recipe_categories_core_companies_id",
                table: "recipe_categories",
                column: "core_companies_id");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_categories_core_companies_id_parent_id",
                table: "recipe_categories",
                columns: new[] { "core_companies_id", "parent_id" });

            migrationBuilder.CreateIndex(
                name: "IX_recipe_categories_core_companies_id_slug",
                table: "recipe_categories",
                columns: new[] { "core_companies_id", "slug" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_recipe_categories_core_companies_id_status",
                table: "recipe_categories",
                columns: new[] { "core_companies_id", "status" });

            migrationBuilder.CreateIndex(
                name: "IX_recipe_categories_deleted_at",
                table: "recipe_categories",
                column: "deleted_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recipe_categories");
        }
    }
}
