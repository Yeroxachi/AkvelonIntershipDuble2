using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkvelonIntershipDuble2.Migrations
{
    public partial class taskStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskStatus",
                table: "Tasks",
                newName: "ProjectTaskStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectTaskStatus",
                table: "Tasks",
                newName: "TaskStatus");
        }
    }
}
