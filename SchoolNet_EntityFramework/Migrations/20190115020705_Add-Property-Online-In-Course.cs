using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolNet_EntityFramework.Migrations
{
    public partial class AddPropertyOnlineInCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Online",
                table: "Course",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Online",
                table: "Course");
        }
    }
}
