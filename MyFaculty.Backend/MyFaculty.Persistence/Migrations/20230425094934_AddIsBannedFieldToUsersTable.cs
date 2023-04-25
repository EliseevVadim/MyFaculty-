using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddIsBannedFieldToUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 25, 12, 49, 33, 226, DateTimeKind.Local).AddTicks(3614));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 25, 12, 49, 33, 230, DateTimeKind.Local).AddTicks(8963));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 25, 12, 49, 33, 230, DateTimeKind.Local).AddTicks(8988));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 4, 25, 12, 49, 33, 230, DateTimeKind.Local).AddTicks(8992));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 4, 25, 12, 49, 33, 230, DateTimeKind.Local).AddTicks(8994));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 4, 25, 12, 49, 33, 230, DateTimeKind.Local).AddTicks(8996));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 25, 12, 49, 33, 291, DateTimeKind.Local).AddTicks(5581));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 25, 12, 49, 33, 291, DateTimeKind.Local).AddTicks(6287));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 25, 12, 49, 33, 291, DateTimeKind.Local).AddTicks(6297));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "users");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 16, 12, 39, 15, 123, DateTimeKind.Local).AddTicks(2064));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 16, 12, 39, 15, 127, DateTimeKind.Local).AddTicks(4444));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 16, 12, 39, 15, 127, DateTimeKind.Local).AddTicks(4493));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 4, 16, 12, 39, 15, 127, DateTimeKind.Local).AddTicks(4500));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 4, 16, 12, 39, 15, 127, DateTimeKind.Local).AddTicks(4504));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 4, 16, 12, 39, 15, 127, DateTimeKind.Local).AddTicks(4508));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 16, 12, 39, 15, 325, DateTimeKind.Local).AddTicks(9399));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 16, 12, 39, 15, 326, DateTimeKind.Local).AddTicks(188));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 16, 12, 39, 15, 326, DateTimeKind.Local).AddTicks(199));
        }
    }
}
