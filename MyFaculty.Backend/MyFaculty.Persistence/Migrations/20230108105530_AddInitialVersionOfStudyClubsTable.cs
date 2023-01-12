using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddInitialVersionOfStudtClubsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudyClub",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClubName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagePath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyClub", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyClub_users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsersAtStudyClubs",
                columns: table => new
                {
                    MembersId = table.Column<int>(type: "int", nullable: false),
                    StudyClubsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAtStudyClubs", x => new { x.MembersId, x.StudyClubsId });
                    table.ForeignKey(
                        name: "FK_UsersAtStudyClubs_StudyClub_StudyClubsId",
                        column: x => x.StudyClubsId,
                        principalTable: "StudyClub",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersAtStudyClubs_users_MembersId",
                        column: x => x.MembersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsersModeratesClubs",
                columns: table => new
                {
                    ModeratorsId = table.Column<int>(type: "int", nullable: false),
                    StudyClubsAtModerationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersModeratesClubs", x => new { x.ModeratorsId, x.StudyClubsAtModerationId });
                    table.ForeignKey(
                        name: "FK_UsersModeratesClubs_StudyClub_StudyClubsAtModerationId",
                        column: x => x.StudyClubsAtModerationId,
                        principalTable: "StudyClub",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersModeratesClubs_users_ModeratorsId",
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
                value: new DateTime(2023, 1, 8, 13, 55, 29, 248, DateTimeKind.Local).AddTicks(3961));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 8, 13, 55, 29, 251, DateTimeKind.Local).AddTicks(8324));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 8, 13, 55, 29, 251, DateTimeKind.Local).AddTicks(8349));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 1, 8, 13, 55, 29, 251, DateTimeKind.Local).AddTicks(8352));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 1, 8, 13, 55, 29, 251, DateTimeKind.Local).AddTicks(8353));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 1, 8, 13, 55, 29, 251, DateTimeKind.Local).AddTicks(8356));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 8, 13, 55, 29, 286, DateTimeKind.Local).AddTicks(6312));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 8, 13, 55, 29, 286, DateTimeKind.Local).AddTicks(6829));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 8, 13, 55, 29, 286, DateTimeKind.Local).AddTicks(6837));

            migrationBuilder.CreateIndex(
                name: "IX_StudyClub_ClubName",
                table: "StudyClub",
                column: "ClubName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudyClub_OwnerId",
                table: "StudyClub",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersAtStudyClubs_StudyClubsId",
                table: "UsersAtStudyClubs",
                column: "StudyClubsId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersModeratesClubs_StudyClubsAtModerationId",
                table: "UsersModeratesClubs",
                column: "StudyClubsAtModerationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersAtStudyClubs");

            migrationBuilder.DropTable(
                name: "UsersModeratedClubs");

            migrationBuilder.DropTable(
                name: "StudyClub");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 6, 17, 30, 47, 225, DateTimeKind.Local).AddTicks(4156));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 6, 17, 30, 47, 226, DateTimeKind.Local).AddTicks(6558));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 6, 17, 30, 47, 226, DateTimeKind.Local).AddTicks(6583));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 1, 6, 17, 30, 47, 226, DateTimeKind.Local).AddTicks(6586));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 1, 6, 17, 30, 47, 226, DateTimeKind.Local).AddTicks(6589));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 1, 6, 17, 30, 47, 226, DateTimeKind.Local).AddTicks(6591));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 6, 17, 30, 47, 275, DateTimeKind.Local).AddTicks(9366));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 6, 17, 30, 47, 275, DateTimeKind.Local).AddTicks(9982));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 6, 17, 30, 47, 275, DateTimeKind.Local).AddTicks(9991));
        }
    }
}
