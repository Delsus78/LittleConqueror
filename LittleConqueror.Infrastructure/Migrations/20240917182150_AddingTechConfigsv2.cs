using LittleConqueror.AppService.Domain.Models.Configs;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingTechConfigsv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TechResearchConfigs",
                table: "Configs",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(TechConfig[]),
                oldType: "jsonb");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$gUWfGiPXnSXV1H8C7KyzpO.npk29c64PAtnUPD49XS0dIENHKarvq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TechConfig[]>(
                name: "TechResearchConfigs",
                table: "Configs",
                type: "jsonb",
                nullable: false,
                defaultValue: new TechConfig[0],
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$aOvaechO0xmTmzNXUbTTMexs76ed3Rtxn3mYpntslYH37gj0Nr7AW");
        }
    }
}
