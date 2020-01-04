using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoListAPI.Migrations
{
    public partial class ToDoListDeleteToDoItemIdField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToDoItemId",
                table: "ToDoLists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToDoItemId",
                table: "ToDoLists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
