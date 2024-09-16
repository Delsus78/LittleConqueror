using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditingTechResearchDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ResearchDate",
                table: "TechResearches",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$J0dGdaY0/HnZeAwSrMv3LuDSQtKiZTDoGU7P6WNTS06mjcrk.DaPS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ResearchDate",
                table: "TechResearches",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$cb11Ivls5bLpb86FeOYP7OVG9Xy2OVecupKN2xly/NvI77NiKUK/.");
        }
    }
}
