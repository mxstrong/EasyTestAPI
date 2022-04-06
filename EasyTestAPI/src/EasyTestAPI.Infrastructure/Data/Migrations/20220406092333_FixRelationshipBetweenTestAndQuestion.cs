using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTestAPI.Infrastructure.Data.Migrations
{
    public partial class FixRelationshipBetweenTestAndQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Tests_TestId",
                table: "Question");

            migrationBuilder.AlterColumn<string>(
                name: "TestId",
                table: "Question",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Tests_TestId",
                table: "Question",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "TestId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Tests_TestId",
                table: "Question");

            migrationBuilder.AlterColumn<string>(
                name: "TestId",
                table: "Question",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Tests_TestId",
                table: "Question",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "TestId");
        }
    }
}
