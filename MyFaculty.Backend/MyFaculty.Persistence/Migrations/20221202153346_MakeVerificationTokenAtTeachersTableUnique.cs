using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFaculty.Persistence.Migrations
{
    public partial class MakeVerificationTokenAtTeachersTableUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Teachers_VerifiactionToken",
                table: "Teachers",
                column: "VerifiactionToken",
                unique: true);

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 33, 45, 557, DateTimeKind.Local).AddTicks(9128));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 33, 45, 559, DateTimeKind.Local).AddTicks(1915));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 33, 45, 559, DateTimeKind.Local).AddTicks(1941));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 33, 45, 559, DateTimeKind.Local).AddTicks(1946));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 33, 45, 559, DateTimeKind.Local).AddTicks(1948));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 33, 45, 559, DateTimeKind.Local).AddTicks(1950));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 33, 45, 621, DateTimeKind.Local).AddTicks(2984));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 33, 45, 621, DateTimeKind.Local).AddTicks(3692));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 12, 2, 18, 33, 45, 621, DateTimeKind.Local).AddTicks(3704));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
