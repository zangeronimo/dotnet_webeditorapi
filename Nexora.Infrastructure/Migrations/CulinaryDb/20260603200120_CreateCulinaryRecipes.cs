using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexora.Infrastructure.Migrations.CulinaryDb
{
    /// <inheritdoc />
    public partial class CreateCulinaryRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "culinary_recipes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    short_description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    full_description = table.Column<string>(type: "text", nullable: false),
                    ingredients = table.Column<string>(type: "jsonb", nullable: false),
                    steps = table.Column<string>(type: "jsonb", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    prep_time = table.Column<int>(type: "integer", nullable: false),
                    cook_time = table.Column<int>(type: "integer", nullable: false),
                    rest_time = table.Column<int>(type: "integer", nullable: false),
                    yield_total = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    difficulty = table.Column<int>(type: "integer", nullable: false),
                    cuisine = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    image_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    meta_title = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    meta_description = table.Column<string>(type: "character varying(170)", maxLength: 170, nullable: false),
                    canonical_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    structured_data = table.Column<string>(type: "jsonb", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    published_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    culinary_category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    core_companies_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tag_ids = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_culinary_recipes", x => x.id);
                    table.ForeignKey(
                        name: "FK_culinary_recipes_culinary_categories_culinary_category_id",
                        column: x => x.culinary_category_id,
                        principalTable: "culinary_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_culinary_recipes_core_companies_id",
                table: "culinary_recipes",
                column: "core_companies_id");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_recipes_core_companies_id_culinary_category_id",
                table: "culinary_recipes",
                columns: new[] { "core_companies_id", "culinary_category_id" });

            migrationBuilder.CreateIndex(
                name: "IX_culinary_recipes_core_companies_id_slug",
                table: "culinary_recipes",
                columns: new[] { "core_companies_id", "slug" },
                unique: true,
                filter: "\"deleted_at\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_recipes_core_companies_id_status",
                table: "culinary_recipes",
                columns: new[] { "core_companies_id", "status" });

            migrationBuilder.CreateIndex(
                name: "IX_culinary_recipes_culinary_category_id",
                table: "culinary_recipes",
                column: "culinary_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_recipes_deleted_at",
                table: "culinary_recipes",
                column: "deleted_at");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_recipes_published_at",
                table: "culinary_recipes",
                column: "published_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "culinary_recipes");
        }
    }
}
