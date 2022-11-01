using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFaculty.Identity.Migrations
{
    public partial class RemoveCountryIdFromUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Users",
                type: "int",
                nullable: true);
        }
    }
}
