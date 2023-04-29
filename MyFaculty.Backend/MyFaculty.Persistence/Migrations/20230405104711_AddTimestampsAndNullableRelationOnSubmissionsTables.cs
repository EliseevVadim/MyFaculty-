using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddTimestampsAndNullableRelationOnSubmissionsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmissions_TaskSubmissionReviews_ReviewId",
                table: "TaskSubmissions");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "TaskSubmissions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "TaskSubmissions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "TaskSubmissions",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "TaskSubmissionReviews",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "TaskSubmissionReviews",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 5, 13, 47, 10, 441, DateTimeKind.Local).AddTicks(6412));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 5, 13, 47, 10, 444, DateTimeKind.Local).AddTicks(8031));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 5, 13, 47, 10, 444, DateTimeKind.Local).AddTicks(8089));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 4, 5, 13, 47, 10, 444, DateTimeKind.Local).AddTicks(8095));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 4, 5, 13, 47, 10, 444, DateTimeKind.Local).AddTicks(8101));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 4, 5, 13, 47, 10, 444, DateTimeKind.Local).AddTicks(8106));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 5, 13, 47, 10, 551, DateTimeKind.Local).AddTicks(833));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 5, 13, 47, 10, 551, DateTimeKind.Local).AddTicks(1753));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 5, 13, 47, 10, 551, DateTimeKind.Local).AddTicks(1767));

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmissions_TaskSubmissionReviews_ReviewId",
                table: "TaskSubmissions",
                column: "ReviewId",
                principalTable: "TaskSubmissionReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmissions_TaskSubmissionReviews_ReviewId",
                table: "TaskSubmissions");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "TaskSubmissions");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "TaskSubmissions");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "TaskSubmissionReviews");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "TaskSubmissionReviews");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "TaskSubmissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 5, 12, 11, 56, 598, DateTimeKind.Local).AddTicks(7576));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 5, 12, 11, 56, 601, DateTimeKind.Local).AddTicks(8706));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 5, 12, 11, 56, 601, DateTimeKind.Local).AddTicks(8734));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 4, 5, 12, 11, 56, 601, DateTimeKind.Local).AddTicks(8737));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 4, 5, 12, 11, 56, 601, DateTimeKind.Local).AddTicks(8740));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 4, 5, 12, 11, 56, 601, DateTimeKind.Local).AddTicks(8742));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 5, 12, 11, 56, 656, DateTimeKind.Local).AddTicks(1204));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 5, 12, 11, 56, 656, DateTimeKind.Local).AddTicks(1805));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 5, 12, 11, 56, 656, DateTimeKind.Local).AddTicks(1815));

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmissions_TaskSubmissionReviews_ReviewId",
                table: "TaskSubmissions",
                column: "ReviewId",
                principalTable: "TaskSubmissionReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
