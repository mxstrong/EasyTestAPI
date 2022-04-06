using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTestAPI.Infrastructure.Data.Migrations
{
    public partial class FixForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionTypes_QuestionTypeId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_QuestionTypeId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "QuestionTypeId",
                table: "Question");

            migrationBuilder.CreateIndex(
                name: "IX_Question_TypeId",
                table: "Question",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuestionTypes_TypeId",
                table: "Question",
                column: "TypeId",
                principalTable: "QuestionTypes",
                principalColumn: "QuestionTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuestionTypes_TypeId",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_TypeId",
                table: "Question");

            migrationBuilder.AddColumn<string>(
                name: "QuestionTypeId",
                table: "Question",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionTypeId",
                table: "Question",
                column: "QuestionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuestionTypes_QuestionTypeId",
                table: "Question",
                column: "QuestionTypeId",
                principalTable: "QuestionTypes",
                principalColumn: "QuestionTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
