using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddPostAttachmentsUidColoumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PostAttachmentsUid",
                table: "posts",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 28, 17, 16, 39, 816, DateTimeKind.Local).AddTicks(6485));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 28, 17, 16, 39, 822, DateTimeKind.Local).AddTicks(4560));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 28, 17, 16, 39, 822, DateTimeKind.Local).AddTicks(4617));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 1, 28, 17, 16, 39, 822, DateTimeKind.Local).AddTicks(4624));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 1, 28, 17, 16, 39, 822, DateTimeKind.Local).AddTicks(4628));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 1, 28, 17, 16, 39, 822, DateTimeKind.Local).AddTicks(4632));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 28, 17, 16, 39, 994, DateTimeKind.Local).AddTicks(3194));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 28, 17, 16, 39, 994, DateTimeKind.Local).AddTicks(4128));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 28, 17, 16, 39, 994, DateTimeKind.Local).AddTicks(4143));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostAttachmentsUid",
                table: "posts");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 25, 12, 36, 13, 457, DateTimeKind.Local).AddTicks(2483));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 25, 12, 36, 13, 459, DateTimeKind.Local).AddTicks(9843));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 25, 12, 36, 13, 459, DateTimeKind.Local).AddTicks(9893));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 1, 25, 12, 36, 13, 459, DateTimeKind.Local).AddTicks(9899));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 1, 25, 12, 36, 13, 459, DateTimeKind.Local).AddTicks(9904));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 1, 25, 12, 36, 13, 459, DateTimeKind.Local).AddTicks(9908));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 25, 12, 36, 13, 686, DateTimeKind.Local).AddTicks(614));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 25, 12, 36, 13, 686, DateTimeKind.Local).AddTicks(1556));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 25, 12, 36, 13, 686, DateTimeKind.Local).AddTicks(1572));
        }
    }
}
