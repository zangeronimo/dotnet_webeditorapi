using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable

namespace Nexora.Infrastructure.Migrations.CulinaryDb
{
    /// <inheritdoc />
    public partial class AddSectionsToRecipeContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ingredients",
                table: "culinary_recipes");

            migrationBuilder.RenameColumn(
                name: "steps",
                table: "culinary_recipes",
                newName: "sections");

            migrationBuilder.DropColumn(
                name: "notes",
                table: "culinary_recipes");

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "culinary_recipes",
                type: "jsonb",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sections",
                table: "culinary_recipes",
                newName: "steps");

            migrationBuilder.DropColumn(
                name: "notes",
                table: "culinary_recipes");

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "culinary_recipes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ingredients",
                table: "culinary_recipes",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }
    }
}
