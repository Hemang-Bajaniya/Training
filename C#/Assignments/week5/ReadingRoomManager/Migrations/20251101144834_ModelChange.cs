using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadingRoomManager.Migrations
{
    /// <inheritdoc />
    public partial class ModelChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Reservations",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "start",
                table: "Reservations",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "end",
                table: "Reservations",
                newName: "End");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Reservations",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Reservations",
                newName: "start");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Reservations",
                newName: "end");
        }
    }
}
