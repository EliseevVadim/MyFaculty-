using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddFacultyIdColoumnToCoursesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 10, 8, 15, 17, 30, 212, DateTimeKind.Local).AddTicks(4494));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 10, 8, 15, 17, 30, 215, DateTimeKind.Local).AddTicks(4383));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 10, 8, 15, 17, 30, 215, DateTimeKind.Local).AddTicks(4432));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2022, 10, 8, 15, 17, 30, 215, DateTimeKind.Local).AddTicks(4437));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2022, 10, 8, 15, 17, 30, 215, DateTimeKind.Local).AddTicks(4440));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2022, 10, 8, 15, 17, 30, 215, DateTimeKind.Local).AddTicks(4444));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 10, 8, 15, 17, 30, 304, DateTimeKind.Local).AddTicks(3618));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 10, 8, 15, 17, 30, 304, DateTimeKind.Local).AddTicks(4989));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 10, 8, 15, 17, 30, 304, DateTimeKind.Local).AddTicks(5003));

            migrationBuilder.CreateIndex(
                name: "IX_Courses_FacultyId",
                table: "Courses",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Faculties_FacultyId",
                table: "Courses",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict,
                onUpdate: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Faculties_FacultyId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_FacultyId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 9, 23, 20, 30, 40, 161, DateTimeKind.Local).AddTicks(5334));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 9, 23, 20, 30, 40, 162, DateTimeKind.Local).AddTicks(9466));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 9, 23, 20, 30, 40, 162, DateTimeKind.Local).AddTicks(9507));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2022, 9, 23, 20, 30, 40, 162, DateTimeKind.Local).AddTicks(9510));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2022, 9, 23, 20, 30, 40, 162, DateTimeKind.Local).AddTicks(9512));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2022, 9, 23, 20, 30, 40, 162, DateTimeKind.Local).AddTicks(9514));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 9, 23, 20, 30, 40, 229, DateTimeKind.Local).AddTicks(7800));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 9, 23, 20, 30, 40, 229, DateTimeKind.Local).AddTicks(8398));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 9, 23, 20, 30, 40, 229, DateTimeKind.Local).AddTicks(8407));
        }
    }
}
