using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCouncilTracker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionEventActions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "event_actions",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "event_actions");
        }
    }
}
