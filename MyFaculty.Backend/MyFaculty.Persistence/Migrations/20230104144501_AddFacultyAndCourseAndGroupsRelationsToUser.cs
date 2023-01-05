using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddFacultyAndCourseAndGroupsRelationsToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "users",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 4, 17, 45, 0, 942, DateTimeKind.Local).AddTicks(5204));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 4, 17, 45, 0, 946, DateTimeKind.Local).AddTicks(1786));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 4, 17, 45, 0, 946, DateTimeKind.Local).AddTicks(1816));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 1, 4, 17, 45, 0, 946, DateTimeKind.Local).AddTicks(1820));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 1, 4, 17, 45, 0, 946, DateTimeKind.Local).AddTicks(1823));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 1, 4, 17, 45, 0, 946, DateTimeKind.Local).AddTicks(1824));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 4, 17, 45, 1, 7, DateTimeKind.Local).AddTicks(1927));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 4, 17, 45, 1, 7, DateTimeKind.Local).AddTicks(2501));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 4, 17, 45, 1, 7, DateTimeKind.Local).AddTicks(2509));

            migrationBuilder.CreateIndex(
                name: "IX_users_CourseId",
                table: "users",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_users_FacultyId",
                table: "users",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_users_GroupId",
                table: "users",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Courses_CourseId",
                table: "users",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_Faculties_FacultyId",
                table: "users",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_Groups_GroupId",
                table: "users",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Courses_CourseId",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_Faculties_FacultyId",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_Groups_GroupId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_CourseId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_FacultyId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_GroupId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "users");

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
    }
}
