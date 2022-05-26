using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTestAPI.Infrastructure.Data.Migrations
{
    public partial class RemoveNullableUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnsweredTests_Users_UserId",
                table: "AnsweredTests");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AnsweredTests",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnsweredTests_Users_UserId",
                table: "AnsweredTests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnsweredTests_Users_UserId",
                table: "AnsweredTests");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AnsweredTests",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_AnsweredTests_Users_UserId",
                table: "AnsweredTests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
