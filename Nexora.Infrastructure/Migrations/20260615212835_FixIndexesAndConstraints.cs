using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable

namespace Nexora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixIndexesAndConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_core_permissions_code",
                table: "core_permissions",
                column: "code",
                unique: true,
                filter: "\"deleted_at\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_core_modules_name",
                table: "core_modules",
                column: "name",
                unique: true,
                filter: "\"deleted_at\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_core_companies_name",
                table: "core_companies",
                column: "name",
                unique: true,
                filter: "\"deleted_at\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_core_permissions_code",
                table: "core_permissions");

            migrationBuilder.DropIndex(
                name: "IX_core_modules_name",
                table: "core_modules");

            migrationBuilder.DropIndex(
                name: "IX_core_companies_name",
                table: "core_companies");
        }
    }
}
