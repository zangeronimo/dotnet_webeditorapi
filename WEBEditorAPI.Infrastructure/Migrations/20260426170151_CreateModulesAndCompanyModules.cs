using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBEditorAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateModulesAndCompanyModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "core_modules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    status = table.Column<byte>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_modules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "core_company_modules",
                columns: table => new
                {
                    company_id = table.Column<Guid>(type: "uuid", nullable: false),
                    module_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_company_modules", x => new { x.company_id, x.module_id });
                    table.ForeignKey(
                        name: "FK_core_company_modules_core_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "core_companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_core_company_modules_core_modules_module_id",
                        column: x => x.module_id,
                        principalTable: "core_modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_core_company_modules_module_id",
                table: "core_company_modules",
                column: "module_id");

            var companyId = new Guid("ac8cdc5f-45a3-47b7-8d29-0a16d48ca380");
            var moduleId = new Guid("81a4e844-3bb1-4b97-8d31-bb542d8b6b2e");
            var now = new DateTimeOffset(2026, 01, 01, 0, 0, 0, TimeSpan.Zero);

            migrationBuilder.InsertData(
            table: "core_modules",
            columns: new[]
            {
                    "id",
                    "name",
                    "status",
                    "created_at"
            },
            values: new object[]
            {
                    moduleId,
                    "WEBEditor",
                    (byte)1,
                    now
            });

            migrationBuilder.InsertData(
            table: "core_company_modules",
            columns: new[]
            {
                    "company_id",
                    "module_id"
            },
            values: new object[]
            {
                    companyId,
                    moduleId
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "core_company_modules");

            migrationBuilder.DropTable(
                name: "core_modules");
        }
    }
}
