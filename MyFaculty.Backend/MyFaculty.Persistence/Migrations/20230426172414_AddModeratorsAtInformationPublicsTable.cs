using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddModeratorsAtInformationPublicsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersModeratesInformationPublics",
                columns: table => new
                {
                    InformationPublicsAtModerationId = table.Column<int>(type: "int", nullable: false),
                    ModeratorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersModeratesInformationPublics", x => new { x.InformationPublicsAtModerationId, x.ModeratorsId });
                    table.ForeignKey(
                        name: "FK_UsersModeratesInformationPublics_InformationPublics_Informat~",
                        column: x => x.InformationPublicsAtModerationId,
                        principalTable: "InformationPublics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersModeratesInformationPublics_users_ModeratorsId",
                        column: x => x.ModeratorsId,
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
                value: new DateTime(2023, 4, 26, 20, 24, 12, 902, DateTimeKind.Local).AddTicks(5099));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 26, 20, 24, 12, 909, DateTimeKind.Local).AddTicks(7780));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 26, 20, 24, 12, 909, DateTimeKind.Local).AddTicks(7852));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 4, 26, 20, 24, 12, 909, DateTimeKind.Local).AddTicks(7863));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 4, 26, 20, 24, 12, 909, DateTimeKind.Local).AddTicks(7867));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 4, 26, 20, 24, 12, 909, DateTimeKind.Local).AddTicks(7871));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 26, 20, 24, 13, 45, DateTimeKind.Local).AddTicks(1050));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 26, 20, 24, 13, 45, DateTimeKind.Local).AddTicks(2119));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 26, 20, 24, 13, 45, DateTimeKind.Local).AddTicks(2135));

            migrationBuilder.CreateIndex(
                name: "IX_UsersModeratesInformationPublics_ModeratorsId",
                table: "UsersModeratesInformationPublics",
                column: "ModeratorsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersModeratesInformationPublics");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 26, 12, 12, 5, 83, DateTimeKind.Local).AddTicks(545));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 26, 12, 12, 5, 87, DateTimeKind.Local).AddTicks(2331));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 26, 12, 12, 5, 87, DateTimeKind.Local).AddTicks(2366));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 4, 26, 12, 12, 5, 87, DateTimeKind.Local).AddTicks(2370));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 4, 26, 12, 12, 5, 87, DateTimeKind.Local).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 4, 26, 12, 12, 5, 87, DateTimeKind.Local).AddTicks(2374));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 4, 26, 12, 12, 5, 162, DateTimeKind.Local).AddTicks(5917));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 4, 26, 12, 12, 5, 162, DateTimeKind.Local).AddTicks(6485));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 4, 26, 12, 12, 5, 162, DateTimeKind.Local).AddTicks(6494));
        }
    }
}
