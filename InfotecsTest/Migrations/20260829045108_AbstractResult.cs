using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfotecsTest.Migrations
{
    /// <inheritdoc />
    public partial class AbstractResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Results_Resutl_Id",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Reports_Report_Id",
                table: "Results");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Results",
                table: "Results");

            migrationBuilder.RenameTable(
                name: "Results",
                newName: "BaseResult");

            migrationBuilder.RenameIndex(
                name: "IX_Results_Report_Id",
                table: "BaseResult",
                newName: "IX_BaseResult_Report_Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "BaseResult",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<double>(
                name: "MinValue",
                table: "BaseResult",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "MedianValie",
                table: "BaseResult",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<double>(
                name: "MaxValue",
                table: "BaseResult",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duration",
                table: "BaseResult",
                type: "interval",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AlterColumn<double>(
                name: "AverageValue",
                table: "BaseResult",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "AverageExicutionTime",
                table: "BaseResult",
                type: "interval",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseResult",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseResult",
                table: "BaseResult",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseResult_Reports_Report_Id",
                table: "BaseResult",
                column: "Report_Id",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_BaseResult_Resutl_Id",
                table: "Reports",
                column: "Resutl_Id",
                principalTable: "BaseResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseResult_Reports_Report_Id",
                table: "BaseResult");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_BaseResult_Resutl_Id",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseResult",
                table: "BaseResult");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseResult");

            migrationBuilder.RenameTable(
                name: "BaseResult",
                newName: "Results");

            migrationBuilder.RenameIndex(
                name: "IX_BaseResult_Report_Id",
                table: "Results",
                newName: "IX_Results_Report_Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Results",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MinValue",
                table: "Results",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MedianValie",
                table: "Results",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "MaxValue",
                table: "Results",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duration",
                table: "Results",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "AverageValue",
                table: "Results",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "AverageExicutionTime",
                table: "Results",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Results",
                table: "Results",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Results_Resutl_Id",
                table: "Reports",
                column: "Resutl_Id",
                principalTable: "Results",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Reports_Report_Id",
                table: "Results",
                column: "Report_Id",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
