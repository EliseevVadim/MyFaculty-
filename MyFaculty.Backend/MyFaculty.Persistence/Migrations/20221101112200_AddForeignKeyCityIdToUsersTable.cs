using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddForeignKeyCityIdToUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onUpdate: ReferentialAction.Cascade,
                onDelete: ReferentialAction.SetNull
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 1, 14, 4, 47, 921, DateTimeKind.Local).AddTicks(6771));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 11, 1, 14, 4, 47, 925, DateTimeKind.Local).AddTicks(4864));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 11, 1, 14, 4, 47, 925, DateTimeKind.Local).AddTicks(4901));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2022, 11, 1, 14, 4, 47, 925, DateTimeKind.Local).AddTicks(4905));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2022, 11, 1, 14, 4, 47, 925, DateTimeKind.Local).AddTicks(4906));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2022, 11, 1, 14, 4, 47, 925, DateTimeKind.Local).AddTicks(4908));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 11, 1, 14, 4, 48, 39, DateTimeKind.Local).AddTicks(5635));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 11, 1, 14, 4, 48, 39, DateTimeKind.Local).AddTicks(6329));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 11, 1, 14, 4, 48, 39, DateTimeKind.Local).AddTicks(6341));
        }
    }
}
