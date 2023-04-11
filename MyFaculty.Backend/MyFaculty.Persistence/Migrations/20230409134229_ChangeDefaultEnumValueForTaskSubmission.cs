using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFaculty.Persistence.Migrations
{
    public partial class ChangeDefaultEnumValueForTaskSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TaskSubmissions",
                type: "longtext",
                nullable: false,
                defaultValue: "SentForEvaluation",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldDefaultValue: "PendingSubmission")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 9, 16, 42, 28, 112, DateTimeKind.Local).AddTicks(8051));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 9, 16, 42, 28, 118, DateTimeKind.Local).AddTicks(182));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 9, 16, 42, 28, 118, DateTimeKind.Local).AddTicks(215));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 4, 9, 16, 42, 28, 118, DateTimeKind.Local).AddTicks(218));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 4, 9, 16, 42, 28, 118, DateTimeKind.Local).AddTicks(221));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 4, 9, 16, 42, 28, 118, DateTimeKind.Local).AddTicks(225));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 9, 16, 42, 28, 230, DateTimeKind.Local).AddTicks(1180));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 9, 16, 42, 28, 230, DateTimeKind.Local).AddTicks(1888));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 9, 16, 42, 28, 230, DateTimeKind.Local).AddTicks(1904));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TaskSubmissions",
                type: "longtext",
                nullable: false,
                defaultValue: "PendingSubmission",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldDefaultValue: "SentForEvaluation")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 6, 10, 2, 44, 902, DateTimeKind.Local).AddTicks(4875));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 6, 10, 2, 44, 905, DateTimeKind.Local).AddTicks(7824));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 6, 10, 2, 44, 905, DateTimeKind.Local).AddTicks(7856));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 4, 6, 10, 2, 44, 905, DateTimeKind.Local).AddTicks(7859));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 4, 6, 10, 2, 44, 905, DateTimeKind.Local).AddTicks(7861));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 4, 6, 10, 2, 44, 905, DateTimeKind.Local).AddTicks(7863));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 6, 10, 2, 44, 960, DateTimeKind.Local).AddTicks(7017));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 6, 10, 2, 44, 960, DateTimeKind.Local).AddTicks(7595));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 6, 10, 2, 44, 960, DateTimeKind.Local).AddTicks(7604));
        }
    }
}
