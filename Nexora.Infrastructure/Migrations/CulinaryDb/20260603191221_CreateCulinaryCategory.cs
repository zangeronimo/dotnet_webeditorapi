using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexora.Infrastructure.Migrations.CulinaryDb
{
    /// <inheritdoc />
    public partial class CreateCulinaryCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "culinary_categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    meta_title = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: true),
                    meta_description = table.Column<string>(type: "character varying(170)", maxLength: 170, nullable: true),
                    canonical_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    featured_image_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    core_companies_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_culinary_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_culinary_categories_culinary_categories_parent_id",
                        column: x => x.parent_id,
                        principalTable: "culinary_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_culinary_categories_core_companies_id",
                table: "culinary_categories",
                column: "core_companies_id");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_categories_core_companies_id_display_order",
                table: "culinary_categories",
                columns: new[] { "core_companies_id", "display_order" });

            migrationBuilder.CreateIndex(
                name: "IX_culinary_categories_core_companies_id_parent_id",
                table: "culinary_categories",
                columns: new[] { "core_companies_id", "parent_id" });

            migrationBuilder.CreateIndex(
                name: "IX_culinary_categories_core_companies_id_slug",
                table: "culinary_categories",
                columns: new[] { "core_companies_id", "slug" },
                unique: true,
                filter: "\"deleted_at\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_categories_core_companies_id_status",
                table: "culinary_categories",
                columns: new[] { "core_companies_id", "status" });

            migrationBuilder.CreateIndex(
                name: "IX_culinary_categories_deleted_at",
                table: "culinary_categories",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_categories_parent_id",
                table: "culinary_categories",
                column: "parent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "culinary_categories");
        }
    }
}
