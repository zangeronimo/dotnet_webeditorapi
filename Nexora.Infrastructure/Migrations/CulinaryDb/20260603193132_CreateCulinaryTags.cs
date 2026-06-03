using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexora.Infrastructure.Migrations.CulinaryDb
{
    /// <inheritdoc />
    public partial class CreateCulinaryTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "culinary_tags",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    core_companies_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_culinary_tags", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_culinary_tags_core_companies_id",
                table: "culinary_tags",
                column: "core_companies_id");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_tags_core_companies_id_name",
                table: "culinary_tags",
                columns: new[] { "core_companies_id", "name" },
                unique: true,
                filter: "\"deleted_at\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_tags_core_companies_id_slug",
                table: "culinary_tags",
                columns: new[] { "core_companies_id", "slug" },
                unique: true,
                filter: "\"deleted_at\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_culinary_tags_core_companies_id_status",
                table: "culinary_tags",
                columns: new[] { "core_companies_id", "status" });

            migrationBuilder.CreateIndex(
                name: "IX_culinary_tags_deleted_at",
                table: "culinary_tags",
                column: "deleted_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "culinary_tags");
        }
    }
}
