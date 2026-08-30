using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfotecsTest.Migrations
{
    /// <inheritdoc />
    public partial class Rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageExicutionTime",
                table: "BaseResult");

            migrationBuilder.DropColumn(
                name: "AverageValue",
                table: "BaseResult");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseResult");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "BaseResult");

            migrationBuilder.DropColumn(
                name: "MaxValue",
                table: "BaseResult");

            migrationBuilder.DropColumn(
                name: "MedianValie",
                table: "BaseResult");

            migrationBuilder.DropColumn(
                name: "MinValue",
                table: "BaseResult");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "BaseResult");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "BaseMeasurement");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "BaseMeasurement");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseMeasurement");

            migrationBuilder.DropColumn(
                name: "ExecutionTime",
                table: "BaseMeasurement");

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AverageExicutionTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    AverageValue = table.Column<double>(type: "double precision", nullable: false),
                    MedianValie = table.Column<double>(type: "double precision", nullable: false),
                    MaxValue = table.Column<double>(type: "double precision", nullable: false),
                    MinValue = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_BaseResult_Id",
                        column: x => x.Id,
                        principalTable: "BaseResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExecutionTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Data = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Values_BaseMeasurement_Id",
                        column: x => x.Id,
                        principalTable: "BaseMeasurement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Values");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "AverageExicutionTime",
                table: "BaseResult",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AverageValue",
                table: "BaseResult",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseResult",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "BaseResult",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MaxValue",
                table: "BaseResult",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MedianValie",
                table: "BaseResult",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MinValue",
                table: "BaseResult",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartDate",
                table: "BaseResult",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Data",
                table: "BaseMeasurement",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Date",
                table: "BaseMeasurement",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseMeasurement",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ExecutionTime",
                table: "BaseMeasurement",
                type: "interval",
                nullable: true);
        }
    }
}
