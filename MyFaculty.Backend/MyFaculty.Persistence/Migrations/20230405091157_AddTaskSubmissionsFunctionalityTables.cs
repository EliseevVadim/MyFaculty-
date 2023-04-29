using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddTaskSubmissionsFunctionalityTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskSubmissionReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TextContent = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubmissionReviewAttachmentsUid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Attachments = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    SubmissionId = table.Column<int>(type: "int", nullable: false),
                    ReviewerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSubmissionReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskSubmissionReviews_users_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TaskSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TextContent = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubmissionAttachmentsUid = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Attachments = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false, defaultValue: "PendingSubmission")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClubTaskId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskSubmissions_clubTasks_ClubTaskId",
                        column: x => x.ClubTaskId,
                        principalTable: "clubTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskSubmissions_TaskSubmissionReviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "TaskSubmissionReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskSubmissions_users_AuthorId",
                        column: x => x.AuthorId,
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

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmissionReviews_ReviewerId",
                table: "TaskSubmissionReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmissions_AuthorId",
                table: "TaskSubmissions",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmissions_ClubTaskId",
                table: "TaskSubmissions",
                column: "ClubTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmissions_ReviewId",
                table: "TaskSubmissions",
                column: "ReviewId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskSubmissions");

            migrationBuilder.DropTable(
                name: "TaskSubmissionReviews");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 3, 8, 14, 46, 45, 161, DateTimeKind.Local).AddTicks(2765));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 3, 8, 14, 46, 45, 163, DateTimeKind.Local).AddTicks(8110));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 3, 8, 14, 46, 45, 163, DateTimeKind.Local).AddTicks(8137));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2023, 3, 8, 14, 46, 45, 163, DateTimeKind.Local).AddTicks(8140));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2023, 3, 8, 14, 46, 45, 163, DateTimeKind.Local).AddTicks(8143));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2023, 3, 8, 14, 46, 45, 163, DateTimeKind.Local).AddTicks(8145));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 3, 8, 14, 46, 45, 224, DateTimeKind.Local).AddTicks(1016));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 3, 8, 14, 46, 45, 224, DateTimeKind.Local).AddTicks(1588));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2023, 3, 8, 14, 46, 45, 224, DateTimeKind.Local).AddTicks(1597));
        }
    }
}
