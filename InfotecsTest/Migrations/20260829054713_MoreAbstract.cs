using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfotecsTest.Migrations
{
    /// <inheritdoc />
    public partial class MoreAbstract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseResult_Reports_Report_Id",
                table: "BaseResult");

            migrationBuilder.DropForeignKey(
                name: "FK_Values_Reports_Report_Id",
                table: "Values");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Values",
                table: "Values");

            migrationBuilder.RenameTable(
                name: "Values",
                newName: "BaseMeasurement");

            migrationBuilder.RenameIndex(
                name: "IX_Values_Report_Id",
                table: "BaseMeasurement",
                newName: "IX_BaseMeasurement_Report_Id");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ExecutionTime",
                table: "BaseMeasurement",
                type: "interval",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "BaseMeasurement",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<double>(
                name: "Data",
                table: "BaseMeasurement",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseMeasurement",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseMeasurement",
                table: "BaseMeasurement",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BaseReport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    Resutl_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseReport_BaseResult_Resutl_Id",
                        column: x => x.Resutl_Id,
                        principalTable: "BaseResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseReport_Resutl_Id",
                table: "BaseReport",
                column: "Resutl_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMeasurement_BaseReport_Report_Id",
                table: "BaseMeasurement",
                column: "Report_Id",
                principalTable: "BaseReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseResult_BaseReport_Report_Id",
                table: "BaseResult",
                column: "Report_Id",
                principalTable: "BaseReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseMeasurement_BaseReport_Report_Id",
                table: "BaseMeasurement");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseResult_BaseReport_Report_Id",
                table: "BaseResult");

            migrationBuilder.DropTable(
                name: "BaseReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseMeasurement",
                table: "BaseMeasurement");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseMeasurement");

            migrationBuilder.RenameTable(
                name: "BaseMeasurement",
                newName: "Values");

            migrationBuilder.RenameIndex(
                name: "IX_BaseMeasurement_Report_Id",
                table: "Values",
                newName: "IX_Values_Report_Id");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ExecutionTime",
                table: "Values",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Values",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Data",
                table: "Values",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Values",
                table: "Values",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Resutl_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_BaseResult_Resutl_Id",
                        column: x => x.Resutl_Id,
                        principalTable: "BaseResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Resutl_Id",
                table: "Reports",
                column: "Resutl_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseResult_Reports_Report_Id",
                table: "BaseResult",
                column: "Report_Id",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Reports_Report_Id",
                table: "Values",
                column: "Report_Id",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
