using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfotecsTest.Migrations
{
    /// <inheritdoc />
    public partial class FixONeToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseReport_BaseResult_Resutl_Id",
                table: "BaseReport");

            migrationBuilder.DropIndex(
                name: "IX_BaseReport_Resutl_Id",
                table: "BaseReport");

            migrationBuilder.DropColumn(
                name: "Resutl_Id",
                table: "BaseReport");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Resutl_Id",
                table: "BaseReport",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BaseReport_Resutl_Id",
                table: "BaseReport",
                column: "Resutl_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseReport_BaseResult_Resutl_Id",
                table: "BaseReport",
                column: "Resutl_Id",
                principalTable: "BaseResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
