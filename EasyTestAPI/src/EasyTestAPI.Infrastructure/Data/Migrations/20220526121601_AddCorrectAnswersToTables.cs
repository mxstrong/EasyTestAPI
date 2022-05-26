using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTestAPI.Infrastructure.Data.Migrations
{
    public partial class AddCorrectAnswersToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "TestAnswers",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Answers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "TestAnswers");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Answers");
        }
    }
}
