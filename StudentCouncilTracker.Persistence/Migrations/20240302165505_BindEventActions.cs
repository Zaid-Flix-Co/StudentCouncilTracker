using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCouncilTracker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class BindEventActions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "event_id",
                table: "event_actions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_event_actions_event_id",
                table: "event_actions",
                column: "event_id");

            migrationBuilder.AddForeignKey(
                name: "fk_event_actions_events_event_id",
                table: "event_actions",
                column: "event_id",
                principalTable: "events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_event_actions_events_event_id",
                table: "event_actions");

            migrationBuilder.DropIndex(
                name: "ix_event_actions_event_id",
                table: "event_actions");

            migrationBuilder.DropColumn(
                name: "event_id",
                table: "event_actions");
        }
    }
}
