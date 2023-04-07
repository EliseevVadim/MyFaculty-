using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFaculty.Persistence.Migrations
{
    public partial class CorrectRelationBetweenSubmissionsAndReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmissions_TaskSubmissionReviews_ReviewId",
                table: "TaskSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_TaskSubmissions_ReviewId",
                table: "TaskSubmissions");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "TaskSubmissions");

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

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmissionReviews_SubmissionId",
                table: "TaskSubmissionReviews",
                column: "SubmissionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmissionReviews_TaskSubmissions_SubmissionId",
                table: "TaskSubmissionReviews",
                column: "SubmissionId",
                principalTable: "TaskSubmissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmissionReviews_TaskSubmissions_SubmissionId",
                table: "TaskSubmissionReviews");

            migrationBuilder.DropIndex(
                name: "IX_TaskSubmissionReviews_SubmissionId",
                table: "TaskSubmissionReviews");

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "TaskSubmissions",
                type: "int",
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

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmissions_ReviewId",
                table: "TaskSubmissions",
                column: "ReviewId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmissions_TaskSubmissionReviews_ReviewId",
                table: "TaskSubmissions",
                column: "ReviewId",
                principalTable: "TaskSubmissionReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
