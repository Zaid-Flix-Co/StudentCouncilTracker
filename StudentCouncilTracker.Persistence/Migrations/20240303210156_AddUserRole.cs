using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentCouncilTracker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "role_id",
                table: "catalog_users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_user_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    updated_user_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id", "created_date", "created_user_name", "name", "updated_date", "updated_user_name" },
                values: new object[,]
                {
                    { 1, null, null, "Председатель", null, null },
                    { 2, null, null, "Заместитель председателя", null, null },
                    { 3, null, null, "Культурный организатор", null, null },
                    { 4, null, null, "Руководитель направления", null, null },
                    { 5, null, null, "Член студенческого совета", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_catalog_users_role_id",
                table: "catalog_users",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_catalog_users_user_roles_role_id",
                table: "catalog_users",
                column: "role_id",
                principalTable: "user_roles",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_catalog_users_user_roles_role_id",
                table: "catalog_users");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropIndex(
                name: "ix_catalog_users_role_id",
                table: "catalog_users");

            migrationBuilder.DropColumn(
                name: "role_id",
                table: "catalog_users");
        }
    }
}
