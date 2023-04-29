using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddInformationPublicsAndPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InformationPublics",
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
                    table.PrimaryKey("PK_InformationPublics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InformationPublics_users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TextContent = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Attachments = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_posts_users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsersAtInformationPublics",
                columns: table => new
                {
                    InformationPublicsId = table.Column<int>(type: "int", nullable: false),
                    MembersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAtInformationPublics", x => new { x.InformationPublicsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_UsersAtInformationPublics_InformationPublics_InformationPubl~",
                        column: x => x.InformationPublicsId,
                        principalTable: "InformationPublics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersAtInformationPublics_users_MembersId",
                        column: x => x.MembersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clubTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudyClubId = table.Column<int>(type: "int", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clubTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_clubTasks_posts_Id",
                        column: x => x.Id,
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_clubTasks_StudyClubs_StudyClubId",
                        column: x => x.StudyClubId,
                        principalTable: "StudyClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "infoPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudyClubId = table.Column<int>(type: "int", nullable: true),
                    InfoPublicId = table.Column<int>(type: "int", nullable: true),
                    CommentsAllowed = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_infoPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_infoPosts_InformationPublics_InfoPublicId",
                        column: x => x.InfoPublicId,
                        principalTable: "InformationPublics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_infoPosts_posts_Id",
                        column: x => x.Id,
                        principalTable: "posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_infoPosts_StudyClubs_StudyClubId",
                        column: x => x.StudyClubId,
                        principalTable: "StudyClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "userslikedposts",
                columns: table => new
                {
                    LikedPostsId = table.Column<int>(type: "int", nullable: false),
                    LikedUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userslikedposts", x => new { x.LikedPostsId, x.LikedUsersId });
                    table.ForeignKey(
                        name: "FK_userslikedposts_infoPosts_LikedPostsId",
                        column: x => x.LikedPostsId,
                        principalTable: "infoPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userslikedposts_users_LikedUsersId",
                        column: x => x.LikedUsersId,
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
                value: new DateTime(2023, 1, 23, 19, 49, 56, 790, DateTimeKind.Local).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 791, DateTimeKind.Local).AddTicks(7365));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 791, DateTimeKind.Local).AddTicks(7395));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 791, DateTimeKind.Local).AddTicks(7398));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 791, DateTimeKind.Local).AddTicks(7401));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 791, DateTimeKind.Local).AddTicks(7403));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 852, DateTimeKind.Local).AddTicks(7077));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 852, DateTimeKind.Local).AddTicks(8345));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 1, 23, 19, 49, 56, 852, DateTimeKind.Local).AddTicks(8359));

            migrationBuilder.CreateIndex(
                name: "IX_clubTasks_StudyClubId",
                table: "clubTasks",
                column: "StudyClubId");

            migrationBuilder.CreateIndex(
                name: "IX_infoPosts_InfoPublicId",
                table: "infoPosts",
                column: "InfoPublicId");

            migrationBuilder.CreateIndex(
                name: "IX_infoPosts_StudyClubId",
                table: "infoPosts",
                column: "StudyClubId");

            migrationBuilder.CreateIndex(
                name: "IX_InformationPublics_ClubName",
                table: "InformationPublics",
                column: "ClubName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InformationPublics_OwnerId",
                table: "InformationPublics",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_posts_AuthorId",
                table: "posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersAtInformationPublics_MembersId",
                table: "UsersAtInformationPublics",
                column: "MembersId");

            migrationBuilder.CreateIndex(
                name: "IX_userslikedposts_LikedUsersId",
                table: "userslikedposts",
                column: "LikedUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyClubs_users_OwnerId",
                table: "StudyClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersAtStudyClubs_StudyClubs_StudyClubsId",
                table: "UsersAtStudyClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersModeratesClubs_StudyClubs_StudyClubsAtModerationId",
                table: "UsersModeratesClubs");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersModeratesClubs_users_ModeratorsId",
                table: "UsersModeratesClubs");

            migrationBuilder.DropTable(
                name: "clubTasks");

            migrationBuilder.DropTable(
                name: "UsersAtInformationPublics");

            migrationBuilder.DropTable(
                name: "userslikedposts");

            migrationBuilder.DropTable(
                name: "infoPosts");

            migrationBuilder.DropTable(
                name: "InformationPublics");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersModeratesClubs",
                table: "UsersModeratesClubs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyClubs",
                table: "StudyClubs");

            migrationBuilder.RenameTable(
                name: "UsersModeratesClubs",
                newName: "UsersModeratedClubs");

            migrationBuilder.RenameTable(
                name: "StudyClubs",
                newName: "StudyClub");

            migrationBuilder.RenameIndex(
                name: "IX_UsersModeratesClubs_StudyClubsAtModerationId",
                table: "UsersModeratedClubs",
                newName: "IX_UsersModeratedClubs_StudyClubsAtModerationId");

            migrationBuilder.RenameIndex(
                name: "IX_StudyClubs_OwnerId",
                table: "StudyClub",
                newName: "IX_StudyClub_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_StudyClubs_ClubName",
                table: "StudyClub",
                newName: "IX_StudyClub_ClubName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersModeratedClubs",
                table: "UsersModeratedClubs",
                columns: new[] { "ModeratorsId", "StudyClubsAtModerationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyClub",
                table: "StudyClub",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_StudyClub_users_OwnerId",
                table: "StudyClub",
                column: "OwnerId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersAtStudyClubs_StudyClub_StudyClubsId",
                table: "UsersAtStudyClubs",
                column: "StudyClubsId",
                principalTable: "StudyClub",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersModeratedClubs_StudyClub_StudyClubsAtModerationId",
                table: "UsersModeratedClubs",
                column: "StudyClubsAtModerationId",
                principalTable: "StudyClub",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersModeratedClubs_users_ModeratorsId",
                table: "UsersModeratedClubs",
                column: "ModeratorsId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
