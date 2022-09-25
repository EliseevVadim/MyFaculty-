using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddReferenceBetweenStateTransitionsAndAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_ExpertSystemAnswers_ExpertSystemStateTransitions_StateTransitionId",
                table: "ExpertSystemAnswers",
                column: "StateTransitionId",
                principalTable: "ExpertSystemStateTransitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpertSystemStateTransitions_ExpertSystemAnswers_AnswerId",
                table: "ExpertSystemStateTransitions");

            migrationBuilder.DropIndex(
                name: "IX_ExpertSystemStateTransitions_AnswerId",
                table: "ExpertSystemStateTransitions");

            migrationBuilder.DropIndex(
                name: "IX_ExpertSystemStateTransitions_InitialStateId",
                table: "ExpertSystemStateTransitions");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 9, 23, 19, 11, 21, 652, DateTimeKind.Local).AddTicks(9858));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 9, 23, 19, 11, 21, 654, DateTimeKind.Local).AddTicks(3605));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 9, 23, 19, 11, 21, 654, DateTimeKind.Local).AddTicks(3635));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2022, 9, 23, 19, 11, 21, 654, DateTimeKind.Local).AddTicks(3639));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2022, 9, 23, 19, 11, 21, 654, DateTimeKind.Local).AddTicks(3640));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2022, 9, 23, 19, 11, 21, 654, DateTimeKind.Local).AddTicks(3643));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 9, 23, 19, 11, 21, 729, DateTimeKind.Local).AddTicks(5271));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 9, 23, 19, 11, 21, 729, DateTimeKind.Local).AddTicks(5852));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 9, 23, 19, 11, 21, 729, DateTimeKind.Local).AddTicks(5861));

            migrationBuilder.CreateIndex(
                name: "IX_ExpertSystemStateTransitions_InitialStateId",
                table: "ExpertSystemStateTransitions",
                column: "InitialStateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpertSystemStateTransitions_ExpertSystemAnswers_InitialStat~",
                table: "ExpertSystemStateTransitions",
                column: "InitialStateId",
                principalTable: "ExpertSystemAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
