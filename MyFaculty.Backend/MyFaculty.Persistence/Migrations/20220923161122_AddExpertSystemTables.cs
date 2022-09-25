using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFaculty.Persistence.Migrations
{
    public partial class AddExpertSystemTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpertSystemStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Question = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Explanation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertSystemStates", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExpertSystemAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StateTransitionId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertSystemAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertSystemAnswers_ExpertSystemStates_StateId",
                        column: x => x.StateId,
                        principalTable: "ExpertSystemStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ExpertSystemStateTransitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InitialStateId = table.Column<int>(type: "int", nullable: false),
                    FinalStateId = table.Column<int>(type: "int", nullable: false),
                    IsLast = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertSystemStateTransitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpertSystemStateTransitions_ExpertSystemAnswers_InitialStat~",
                        column: x => x.InitialStateId,
                        principalTable: "ExpertSystemAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertSystemStateTransitions_ExpertSystemStates_FinalStateId",
                        column: x => x.FinalStateId,
                        principalTable: "ExpertSystemStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertSystemStateTransitions_ExpertSystemStates_InitialState~",
                        column: x => x.InitialStateId,
                        principalTable: "ExpertSystemStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "IX_ExpertSystemAnswers_StateId",
                table: "ExpertSystemAnswers",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertSystemStateTransitions_FinalStateId",
                table: "ExpertSystemStateTransitions",
                column: "FinalStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertSystemStateTransitions_InitialStateId",
                table: "ExpertSystemStateTransitions",
                column: "InitialStateId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpertSystemStateTransitions");

            migrationBuilder.DropTable(
                name: "ExpertSystemAnswers");

            migrationBuilder.DropTable(
                name: "ExpertSystemStates");

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 9, 16, 10, 50, 15, 286, DateTimeKind.Local).AddTicks(2413));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 9, 16, 10, 50, 15, 287, DateTimeKind.Local).AddTicks(5299));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 9, 16, 10, 50, 15, 287, DateTimeKind.Local).AddTicks(5330));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 4,
                column: "Created",
                value: new DateTime(2022, 9, 16, 10, 50, 15, 287, DateTimeKind.Local).AddTicks(5333));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 5,
                column: "Created",
                value: new DateTime(2022, 9, 16, 10, 50, 15, 287, DateTimeKind.Local).AddTicks(5336));

            migrationBuilder.UpdateData(
                table: "DaysOfWeek",
                keyColumn: "Id",
                keyValue: 6,
                column: "Created",
                value: new DateTime(2022, 9, 16, 10, 50, 15, 287, DateTimeKind.Local).AddTicks(5338));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 9, 16, 10, 50, 15, 316, DateTimeKind.Local).AddTicks(9042));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2022, 9, 16, 10, 50, 15, 317, DateTimeKind.Local).AddTicks(449));

            migrationBuilder.UpdateData(
                table: "PairRepeatings",
                keyColumn: "Id",
                keyValue: 3,
                column: "Created",
                value: new DateTime(2022, 9, 16, 10, 50, 15, 317, DateTimeKind.Local).AddTicks(461));
        }
    }
}
