using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBEditorAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixIndexUniqueError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_core_users_email",
                table: "core_users");

            migrationBuilder.DropIndex(
                name: "IX_core_user_companies_user_id_company_id",
                table: "core_user_companies");

            migrationBuilder.CreateIndex(
                name: "IX_core_users_email",
                table: "core_users",
                column: "email",
                unique: true,
                filter: "\"deleted_at\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_core_user_companies_user_id_company_id",
                table: "core_user_companies",
                columns: new[] { "user_id", "company_id" },
                unique: true,
                filter: "\"deleted_at\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_core_users_email",
                table: "core_users");

            migrationBuilder.DropIndex(
                name: "IX_core_user_companies_user_id_company_id",
                table: "core_user_companies");

            migrationBuilder.CreateIndex(
                name: "IX_core_users_email",
                table: "core_users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_core_user_companies_user_id_company_id",
                table: "core_user_companies",
                columns: new[] { "user_id", "company_id" },
                unique: true);
        }
    }
}
