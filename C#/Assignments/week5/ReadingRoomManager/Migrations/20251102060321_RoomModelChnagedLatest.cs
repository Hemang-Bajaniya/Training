using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReadingRoomManager.Migrations
{
    /// <inheritdoc />
    public partial class RoomModelChnagedLatest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "End", "RoomId", "Start", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 11, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2025, 11, 2, 16, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 11, 2, 14, 0, 0, 0, DateTimeKind.Unspecified), 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
