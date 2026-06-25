using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexora.Infrastructure.Migrations.CulinaryDb
{
    /// <inheritdoc />
    public partial class RemoveCanonicalUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "canonical_url",
                table: "culinary_recipes");

            migrationBuilder.DropColumn(
                name: "canonical_url",
                table: "culinary_categories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "canonical_url",
                table: "culinary_recipes",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "canonical_url",
                table: "culinary_categories",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
