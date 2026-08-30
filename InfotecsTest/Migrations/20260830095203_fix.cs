using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfotecsTest.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BaseResult_Report_Id",
                table: "BaseResult");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "BaseReport",
                type: "character varying(127)",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_BaseResult_Report_Id",
                table: "BaseResult",
                column: "Report_Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BaseResult_Report_Id",
                table: "BaseResult");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "BaseReport",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(127)",
                oldMaxLength: 127);

            migrationBuilder.CreateIndex(
                name: "IX_BaseResult_Report_Id",
                table: "BaseResult",
                column: "Report_Id");
        }
    }
}
