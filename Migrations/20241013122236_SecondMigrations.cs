using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId1",
                table: "Mark",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mark_StudentId1",
                table: "Mark",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Mark_Student_StudentId1",
                table: "Mark",
                column: "StudentId1",
                principalTable: "Student",
                principalColumn: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mark_Student_StudentId1",
                table: "Mark");

            migrationBuilder.DropIndex(
                name: "IX_Mark_StudentId1",
                table: "Mark");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "Mark");
        }
    }
}
