using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MyFaculty.Persistence.Migrations
{
    public partial class RenameNameColoumnAtInformationPublicsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClubName",
                table: "InformationPublics",
                newName: "PublicName");

            migrationBuilder.RenameIndex(
                name: "IX_InformationPublics_ClubName",
                table: "InformationPublics",
                newName: "IX_InformationPublics_PublicName");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 24, 10, 26, 42, 269, DateTimeKind.Local).AddTicks(8069));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 24, 10, 26, 42, 274, DateTimeKind.Local).AddTicks(1132));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 24, 10, 26, 42, 274, DateTimeKind.Local).AddTicks(1177));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 1, 24, 10, 26, 42, 274, DateTimeKind.Local).AddTicks(1182));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 1, 24, 10, 26, 42, 274, DateTimeKind.Local).AddTicks(1185));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 1, 24, 10, 26, 42, 274, DateTimeKind.Local).AddTicks(1189));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 24, 10, 26, 42, 352, DateTimeKind.Local).AddTicks(1632));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 24, 10, 26, 42, 352, DateTimeKind.Local).AddTicks(2524));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 24, 10, 26, 42, 352, DateTimeKind.Local).AddTicks(2534));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicName",
                table: "InformationPublics",
                newName: "ClubName");

            migrationBuilder.RenameIndex(
                name: "IX_InformationPublics_PublicName",
                table: "InformationPublics",
                newName: "IX_InformationPublics_ClubName");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 790, DateTimeKind.Local).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 791, DateTimeKind.Local).AddTicks(7365));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 791, DateTimeKind.Local).AddTicks(7395));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 791, DateTimeKind.Local).AddTicks(7398));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 791, DateTimeKind.Local).AddTicks(7401));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 791, DateTimeKind.Local).AddTicks(7403));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 852, DateTimeKind.Local).AddTicks(7077));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 852, DateTimeKind.Local).AddTicks(8345));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 852, DateTimeKind.Local).AddTicks(8359));
        }
    }
}
