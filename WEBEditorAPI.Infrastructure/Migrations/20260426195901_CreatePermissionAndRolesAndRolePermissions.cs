using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBEditorAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatePermissionAndRolesAndRolePermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "core_permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    label = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    status = table.Column<byte>(type: "smallint", nullable: false),
                    module_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_permissions", x => x.id);
                    table.ForeignKey(
                        name: "FK_core_permissions_core_modules_module_id",
                        column: x => x.module_id,
                        principalTable: "core_modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "core_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    status = table.Column<byte>(type: "smallint", nullable: false),
                    company_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_core_roles_company_id",
                        column: x => x.company_id,
                        principalTable: "core_companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "core_role_permissions",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_role_permissions", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "FK_core_role_permissions_core_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "core_permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_core_role_permissions_core_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "core_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_core_permissions_module_id",
                table: "core_permissions",
                column: "module_id");

            migrationBuilder.CreateIndex(
                name: "IX_core_role_permissions_permission_id",
                table: "core_role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_core_roles_company_id",
                table: "core_roles",
                column: "company_id");

            var companyId = new Guid("ac8cdc5f-45a3-47b7-8d29-0a16d48ca380");
            var moduleId = new Guid("81a4e844-3bb1-4b97-8d31-bb542d8b6b2e");
            var permissionModule1Id = new Guid("af3975e1-2cee-43f1-98c0-d1cb05880a6e");
            var permissionModule2Id = new Guid("273880f9-fee0-40a2-95e1-e80f73562b8b");
            var permissionModule3Id = new Guid("8f31410a-fc61-43cf-86ef-753f75eaf722");
            var permissionModule4Id = new Guid("40d2b3da-1685-4a6c-b4b2-f480ff8df2f9");
            var roleId = new Guid("fb06f52a-9f40-419d-9409-22397da144cb");
            var now = new DateTimeOffset(2026, 01, 01, 0, 0, 0, TimeSpan.Zero);

            migrationBuilder.InsertData(
                table: "core_permissions",
                columns: new[] { "id", "label", "code", "status", "module_id", "created_at" },
                values: new object[,]
                {
                        { permissionModule1Id, "Module View", "module.user.view", (byte)1, moduleId, now },
                        { permissionModule2Id, "Module Update", "module.user.create", (byte)1, moduleId, now },
                        { permissionModule3Id, "Module Update", "module.user.update", (byte)1, moduleId, now },
                        { permissionModule4Id, "Module Delete", "module.user.delete", (byte)1, moduleId, now }
                });

            migrationBuilder.InsertData(
                table: "core_roles",
                columns: new[] { "id", "name", "status", "company_id", "created_at" },
                values: new object[]
                {
                        roleId,
                        "Admin",
                        (byte)1,
                        companyId,
                        now
                });

            migrationBuilder.InsertData(
                table: "core_role_permissions",
                columns: new[] { "role_id", "permission_id" },
                values: new object[,]
                {
                        { roleId, permissionModule1Id },
                        { roleId, permissionModule2Id },
                        { roleId, permissionModule3Id },
                        { roleId, permissionModule4Id}
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "core_role_permissions");

            migrationBuilder.DropTable(
                name: "core_permissions");

            migrationBuilder.DropTable(
                name: "core_roles");
        }
    }
}
