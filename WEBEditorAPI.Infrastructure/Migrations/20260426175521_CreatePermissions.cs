using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBEditorAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatePermissions : Migration
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

            migrationBuilder.CreateIndex(
                name: "IX_core_permissions_module_id",
                table: "core_permissions",
                column: "module_id");

            var moduleId = new Guid("81a4e844-3bb1-4b97-8d31-bb542d8b6b2e");
            var permissionModule1Id = new Guid("af3975e1-2cee-43f1-98c0-d1cb05880a6e");
            var permissionModule2Id = new Guid("273880f9-fee0-40a2-95e1-e80f73562b8b");
            var permissionModule3Id = new Guid("8f31410a-fc61-43cf-86ef-753f75eaf722");
            var now = new DateTimeOffset(2026, 01, 01, 0, 0, 0, TimeSpan.Zero);

            migrationBuilder.InsertData(
                table: "core_permissions",
                columns: new[] { "id", "label", "code", "status", "module_id", "created_at" },
                values: new object[,]
                {
                        { permissionModule1Id, "Module View", "module.view", (byte)1, moduleId, now },
                        { permissionModule2Id, "Module Update", "module.update", (byte)1, moduleId, now },
                        { permissionModule3Id, "Module Delete", "module.delete", (byte)1, moduleId, now }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "core_permissions");
        }
    }
}
