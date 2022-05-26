using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyTestAPI.Infrastructure.Data.Migrations
{
  public partial class FewChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SolvedAt",
                table: "AnsweredTests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SolvedAt",
                table: "AnsweredTests");
        }
    }
}
