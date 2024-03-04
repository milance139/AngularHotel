using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AngularHotel.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeeders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservedRooms",
                table: "ReservedRooms");

            migrationBuilder.AlterColumn<decimal>(
                name: "OriginalPrice",
                table: "Reservations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservedRooms",
                table: "ReservedRooms",
                columns: new[] { "Id", "ReservationId", "RoomId" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "Code", "Description", "IsDeleted", "Name", "RoomType" },
                values: new object[,]
                {
                    { 1, 4, "R1", "Description of Room 1", false, "Room 1", 0 },
                    { 2, 6, "R2", "Description of Room 2", false, "Room 2", 1 },
                    { 3, 2, "R3", "Description of Room 3", false, "Room 3", 2 },
                    { 4, 3, "R4", "Description of Room 4", false, "Room 4", 0 },
                    { 5, 5, "R5", "Description of Room 5", false, "Room 5", 1 },
                    { 6, 4, "R6", "Description of Room 6", false, "Room 6", 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "Email", "LastName", "Name", "PasswordHash", "PasswordSalt", "PhoneNumber", "Role" },
                values: new object[] { 1, new DateTime(2024, 3, 4, 16, 42, 49, 232, DateTimeKind.Local).AddTicks(8028), "admin@live.com", "User", "Admin", new byte[] { 45, 167, 168, 60, 158, 91, 69, 56, 88, 141, 98, 62, 14, 61, 161, 137, 255, 59, 242, 156, 208, 94, 190, 121, 7, 254, 216, 181, 108, 144, 181, 232, 156, 167, 43, 235, 254, 165, 0, 237, 77, 228, 249, 234, 127, 153, 115, 103, 244, 14, 58, 209, 48, 211, 116, 194, 236, 5, 57, 183, 195, 85, 60, 106 }, new byte[] { 54, 228, 210, 83, 109, 41, 174, 168, 107, 240, 120, 105, 254, 27, 29, 45, 236, 160, 140, 66, 176, 127, 198, 151, 25, 183, 151, 139, 254, 100, 79, 237, 165, 123, 212, 52, 81, 167, 154, 185, 166, 124, 98, 40, 162, 37, 198, 250, 96, 47, 85, 230, 83, 127, 182, 185, 76, 175, 223, 59, 53, 247, 215, 129, 102, 51, 14, 161, 232, 16, 190, 154, 30, 95, 154, 238, 10, 64, 39, 118, 243, 249, 153, 210, 1, 64, 213, 240, 53, 217, 31, 96, 7, 162, 151, 145, 152, 152, 85, 248, 172, 161, 37, 92, 227, 30, 122, 174, 10, 29, 122, 16, 54, 134, 201, 102, 171, 70, 153, 38, 178, 21, 190, 4, 153, 123, 109, 197 }, "123456789", "Admin" });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CurrencyId", "Discount", "From", "IsCancelled", "OriginalPrice", "ReservationCommitteeId", "To", "TotalPrice" },
                values: new object[,]
                {
                    { 1, 1, 10, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 100m, 1, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 90m },
                    { 2, 2, 20, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 150m, 1, new DateTime(2024, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 120m },
                    { 3, 1, 0, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 200m, 1, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 200m },
                    { 4, 2, 0, new DateTime(2024, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 100m, 1, new DateTime(2024, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m },
                    { 5, 1, 10, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 180m, 1, new DateTime(2024, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 162m }
                });

            migrationBuilder.InsertData(
                table: "ReservedRooms",
                columns: new[] { "Id", "ReservationId", "RoomId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservedRooms_ReservationId",
                table: "ReservedRooms",
                column: "ReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReservedRooms",
                table: "ReservedRooms");

            migrationBuilder.DropIndex(
                name: "IX_ReservedRooms_ReservationId",
                table: "ReservedRooms");

            migrationBuilder.DeleteData(
                table: "ReservedRooms",
                keyColumns: new[] { "Id", "ReservationId", "RoomId" },
                keyValues: new object[] { 1, 1, 1 });

            migrationBuilder.DeleteData(
                table: "ReservedRooms",
                keyColumns: new[] { "Id", "ReservationId", "RoomId" },
                keyValues: new object[] { 2, 2, 2 });

            migrationBuilder.DeleteData(
                table: "ReservedRooms",
                keyColumns: new[] { "Id", "ReservationId", "RoomId" },
                keyValues: new object[] { 3, 3, 3 });

            migrationBuilder.DeleteData(
                table: "ReservedRooms",
                keyColumns: new[] { "Id", "ReservationId", "RoomId" },
                keyValues: new object[] { 4, 4, 4 });

            migrationBuilder.DeleteData(
                table: "ReservedRooms",
                keyColumns: new[] { "Id", "ReservationId", "RoomId" },
                keyValues: new object[] { 5, 5, 5 });

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "OriginalPrice",
                table: "Reservations",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReservedRooms",
                table: "ReservedRooms",
                columns: new[] { "ReservationId", "RoomId" });
        }
    }
}
