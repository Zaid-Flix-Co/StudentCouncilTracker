using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentCouncilTracker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEventAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "event_action_types",
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
                    table.PrimaryKey("pk_event_action_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "event_actions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    deadline_completion = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    responsible_manager_id = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    event_action_type_id = table.Column<int>(type: "integer", nullable: true),
                    created_user_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    updated_user_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_actions", x => x.id);
                    table.ForeignKey(
                        name: "fk_event_actions_catalog_users_responsible_manager_id",
                        column: x => x.responsible_manager_id,
                        principalTable: "catalog_users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_event_actions_event_action_types_event_action_type_id",
                        column: x => x.event_action_type_id,
                        principalTable: "event_action_types",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "event_types",
                columns: new[] { "id", "created_date", "created_user_name", "name", "updated_date", "updated_user_name" },
                values: new object[,]
                {
                    { 1, null, null, "Концерт", null, null },
                    { 2, null, null, "Малое мероприятие", null, null },
                    { 3, null, null, "Среднее мероприятие", null, null },
                    { 4, null, null, "Большое мероприятие", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_event_actions_event_action_type_id",
                table: "event_actions",
                column: "event_action_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_actions_responsible_manager_id",
                table: "event_actions",
                column: "responsible_manager_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_actions");

            migrationBuilder.DropTable(
                name: "event_action_types");

            migrationBuilder.DeleteData(
                table: "event_types",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "event_types",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "event_types",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "event_types",
                keyColumn: "id",
                keyValue: 4);
        }
    }
}
