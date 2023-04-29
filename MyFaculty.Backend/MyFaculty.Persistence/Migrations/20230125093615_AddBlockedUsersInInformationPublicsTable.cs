using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddBlockedUsersInInformationPublicsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersBlockedInPublics",
                columns: table => new
                {
                    BlockedPublicsId = table.Column<int>(type: "int", nullable: false),
                    BlockedUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersBlockedInPublics", x => new { x.BlockedPublicsId, x.BlockedUsersId });
                    table.ForeignKey(
                        name: "FK_UsersBlockedInPublics_InformationPublics_BlockedPublicsId",
                        column: x => x.BlockedPublicsId,
                        principalTable: "InformationPublics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersBlockedInPublics_users_BlockedUsersId",
                        column: x => x.BlockedUsersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_UsersBlockedInPublics_BlockedUsersId",
                table: "UsersBlockedInPublics",
                column: "BlockedUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersBlockedInPublics");

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
    }
}
