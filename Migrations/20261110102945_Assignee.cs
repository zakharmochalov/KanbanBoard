using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanBoard.Migrations
{
    /// <inheritdoc />
    public partial class Assignee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Assignee",
                table: "TaskCards",
                type: "TEXT",
                maxLength: 30,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assignee",
                table: "TaskCards");
        }
    }
}
