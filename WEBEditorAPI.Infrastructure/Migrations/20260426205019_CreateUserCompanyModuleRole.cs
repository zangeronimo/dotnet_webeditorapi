using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBEditorAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserCompanyModuleRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "core_user_company_roles",
                columns: table => new
                {
                    user_company_id = table.Column<Guid>(type: "uuid", nullable: false),
                    module_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_user_company_roles", x => new { x.user_company_id, x.module_id });
                    table.ForeignKey(
                        name: "FK_core_user_company_roles_core_modules_module_id",
                        column: x => x.module_id,
                        principalTable: "core_modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_core_user_company_roles_core_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "core_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_core_user_company_roles_core_user_companies_user_company_id",
                        column: x => x.user_company_id,
                        principalTable: "core_user_companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_core_user_company_roles_module_id",
                table: "core_user_company_roles",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_core_user_company_roles_role_id",
                table: "core_user_company_roles",
                column: "role_id");

            var userCompanyId = new Guid("9be74cb4-3ccd-4c35-b9dc-c4f783f5dac7");
            var moduleId = new Guid("81a4e844-3bb1-4b97-8d31-bb542d8b6b2e");
            var roleId = new Guid("fb06f52a-9f40-419d-9409-22397da144cb");

            migrationBuilder.InsertData(
                table: "core_user_company_roles",
                columns: new[] { "user_company_id", "module_id", "role_id" },
                values: new object[,]
                {
                    { userCompanyId, moduleId, roleId }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "core_user_company_roles");
        }
    }
}
