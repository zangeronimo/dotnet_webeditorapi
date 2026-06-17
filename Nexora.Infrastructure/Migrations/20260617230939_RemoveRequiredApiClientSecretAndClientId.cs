using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexora.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRequiredApiClientSecretAndClientId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_core_api_clients_core_companies_company_id",
                table: "core_api_clients",
                column: "company_id",
                principalTable: "core_companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_core_api_clients_core_companies_company_id",
                table: "core_api_clients");
        }
    }
}
