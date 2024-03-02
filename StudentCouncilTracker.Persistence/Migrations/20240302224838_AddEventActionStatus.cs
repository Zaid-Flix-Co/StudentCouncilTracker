using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentCouncilTracker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEventActionStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "event_actions");

            migrationBuilder.AddColumn<int>(
                name: "status_id",
                table: "event_actions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "event_action_statuses",
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
                    table.PrimaryKey("pk_event_action_statuses", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "event_action_statuses",
                columns: new[] { "id", "created_date", "created_user_name", "name", "updated_date", "updated_user_name" },
                values: new object[,]
                {
                    { 1, null, null, "Ожидает выполнения", null, null },
                    { 2, null, null, "В работе", null, null },
                    { 3, null, null, "На проверке", null, null },
                    { 4, null, null, "Завершена", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_event_actions_status_id",
                table: "event_actions",
                column: "status_id");

            migrationBuilder.AddForeignKey(
                name: "fk_event_actions_event_action_statuses_status_id",
                table: "event_actions",
                column: "status_id",
                principalTable: "event_action_statuses",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_event_actions_event_action_statuses_status_id",
                table: "event_actions");

            migrationBuilder.DropTable(
                name: "event_action_statuses");

            migrationBuilder.DropIndex(
                name: "ix_event_actions_status_id",
                table: "event_actions");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "event_actions");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "event_actions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
