using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTestAPI.Infrastructure.Data.Migrations
{
    public partial class AddQuestionTypeNavigationProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionTypes_QuestionTypeId",
                table: "Question");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionTypeId",
                table: "Question",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuestionTypes_QuestionTypeId",
                table: "Question",
                column: "QuestionTypeId",
                principalTable: "QuestionTypes",
                principalColumn: "QuestionTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionTypes_QuestionTypeId",
                table: "Question");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionTypeId",
                table: "Question",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuestionTypes_QuestionTypeId",
                table: "Question",
                column: "QuestionTypeId",
                principalTable: "QuestionTypes",
                principalColumn: "QuestionTypeId");
        }
    }
}
