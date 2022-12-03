using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddVerificationTokenToTeachersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VerifiactionToken",
                table: "Teachers",
                type: "char(36)",
                nullable: false,
                defaultValue: Guid.NewGuid(),
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 23, 2, 510, DateTimeKind.Local).AddTicks(1437));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 23, 2, 513, DateTimeKind.Local).AddTicks(1993));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 23, 2, 513, DateTimeKind.Local).AddTicks(2018));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 23, 2, 513, DateTimeKind.Local).AddTicks(2021));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 23, 2, 513, DateTimeKind.Local).AddTicks(2023));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 23, 2, 513, DateTimeKind.Local).AddTicks(2025));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 23, 2, 560, DateTimeKind.Local).AddTicks(3565));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 23, 2, 560, DateTimeKind.Local).AddTicks(4146));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 23, 2, 560, DateTimeKind.Local).AddTicks(4156));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teachers_VerifiactionToken",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "VerifiactionToken",
                table: "Teachers");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 11, 19, 31, 58, 815, DateTimeKind.Local).AddTicks(6880));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 11, 11, 19, 31, 58, 818, DateTimeKind.Local).AddTicks(3273));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 11, 11, 19, 31, 58, 818, DateTimeKind.Local).AddTicks(3307));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2022, 11, 11, 19, 31, 58, 818, DateTimeKind.Local).AddTicks(3311));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2022, 11, 11, 19, 31, 58, 818, DateTimeKind.Local).AddTicks(3314));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2022, 11, 11, 19, 31, 58, 818, DateTimeKind.Local).AddTicks(3316));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 11, 19, 31, 58, 871, DateTimeKind.Local).AddTicks(6071));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 11, 11, 19, 31, 58, 871, DateTimeKind.Local).AddTicks(6825));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 11, 11, 19, 31, 58, 871, DateTimeKind.Local).AddTicks(6834));
        }
    }
}
