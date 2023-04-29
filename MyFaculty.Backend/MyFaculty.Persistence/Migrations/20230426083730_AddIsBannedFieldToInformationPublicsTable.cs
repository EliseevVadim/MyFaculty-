using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddIsBannedFieldToInformationPublicsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "informationpublics",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 26, 11, 37, 29, 164, DateTimeKind.Local).AddTicks(5277));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 26, 11, 37, 29, 168, DateTimeKind.Local).AddTicks(6838));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 26, 11, 37, 29, 168, DateTimeKind.Local).AddTicks(6865));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 4, 26, 11, 37, 29, 168, DateTimeKind.Local).AddTicks(6868));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 4, 26, 11, 37, 29, 168, DateTimeKind.Local).AddTicks(6870));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 4, 26, 11, 37, 29, 168, DateTimeKind.Local).AddTicks(6872));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 26, 11, 37, 29, 242, DateTimeKind.Local).AddTicks(185));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 26, 11, 37, 29, 242, DateTimeKind.Local).AddTicks(725));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 26, 11, 37, 29, 242, DateTimeKind.Local).AddTicks(735));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "informationpublics");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 25, 16, 33, 24, 178, DateTimeKind.Local).AddTicks(818));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 25, 16, 33, 24, 182, DateTimeKind.Local).AddTicks(6980));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 25, 16, 33, 24, 182, DateTimeKind.Local).AddTicks(7036));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 4, 25, 16, 33, 24, 182, DateTimeKind.Local).AddTicks(7044));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 4, 25, 16, 33, 24, 182, DateTimeKind.Local).AddTicks(7048));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 4, 25, 16, 33, 24, 182, DateTimeKind.Local).AddTicks(7052));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 25, 16, 33, 24, 291, DateTimeKind.Local).AddTicks(1324));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 25, 16, 33, 24, 291, DateTimeKind.Local).AddTicks(1957));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 25, 16, 33, 24, 291, DateTimeKind.Local).AddTicks(1967));
        }
    }
}
