using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBEditorAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserCompanyAndUserCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "core_companies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    status = table.Column<byte>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_companies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "core_users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<byte>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "core_user_companies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nickname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    avatar_url = table.Column<string>(type: "text", nullable: true),
                    invited_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    joined_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    last_accessed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<byte>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_core_user_companies", x => x.id);
                    table.ForeignKey(
                        name: "FK_core_user_companies_core_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "core_companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_core_user_companies_core_users_user_id",
                        column: x => x.user_id,
                        principalTable: "core_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_core_user_companies_company_id",
                table: "core_user_companies",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_core_user_companies_user_id_company_id",
                table: "core_user_companies",
                columns: new[] { "user_id", "company_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_core_users_email",
                table: "core_users",
                column: "email",
                unique: true);

            var companyId = new Guid("ac8cdc5f-45a3-47b7-8d29-0a16d48ca380");
            var userId = new Guid("f1b43009-50f1-45a4-bd47-82c6b32d420f");
            var userCompanyId = new Guid("9be74cb4-3ccd-4c35-b9dc-c4f783f5dac7");
            var now = new DateTimeOffset(2026, 01, 01, 0, 0, 0, TimeSpan.Zero);

            migrationBuilder.InsertData(
            table: "core_companies",
            columns: new[]
            {
                    "id",
                    "name",
                    "status",
                    "created_at"
            },
            values: new object[]
            {
                    companyId,
                    "Tudo Linux",
                    (byte)1,
                    now
            });

            migrationBuilder.InsertData(
            table: "core_users",
            columns: new[]
            {
                    "id",
                    "name",
                    "email",
                    "password_hash",
                    "status",
                    "created_at"
            },
            values: new object[]
            {
                    userId,
                    "Zangeronimo",
                    "zangeronimo@gmail.com",
                    "$argon2id$v=19$m=65536,t=3,p=1$w0Q7XYh8i5ultfNpTkPGwQ$5twyriMCFbz9S9R1J2OerrWV9CxqrPC0ofqiOiLVQG4",
                    (byte)1,
                    now
            });

            migrationBuilder.InsertData(
            table: "core_user_companies",
            columns: new[]
            {
                    "id",
                    "user_id",
                    "company_id",
                    "status",
                    "invited_at",
                    "joined_at",
                    "last_accessed_at",
                    "created_at"
            },
            values: new object[]
            {
                    userCompanyId,
                    userId,
                    companyId,
                    (byte)1,
                    now,
                    now,
                    now,
                    now
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "core_user_companies");

            migrationBuilder.DropTable(
                name: "core_companies");

            migrationBuilder.DropTable(
                name: "core_users");
        }
    }
}
