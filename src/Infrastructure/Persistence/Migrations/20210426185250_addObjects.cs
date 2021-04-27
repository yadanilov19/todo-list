using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class addObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reminder",
                table: "TodoItems",
                newName: "ExpiryDate");

            migrationBuilder.AddColumn<int>(
                name: "TodoItemRefId",
                table: "TodoItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_TodoItemRefId",
                table: "TodoItems",
                column: "TodoItemRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_TodoItems_TodoItemRefId",
                table: "TodoItems",
                column: "TodoItemRefId",
                principalTable: "TodoItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_TodoItems_TodoItemRefId",
                table: "TodoItems");

            migrationBuilder.DropIndex(
                name: "IX_TodoItems_TodoItemRefId",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "TodoItemRefId",
                table: "TodoItems");

            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "TodoItems",
                newName: "Reminder");
        }
    }
}
