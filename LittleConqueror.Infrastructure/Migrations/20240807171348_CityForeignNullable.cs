using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CityForeignNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Territories_TerritoryId",
                table: "Cities");

            migrationBuilder.AlterColumn<int>(
                name: "TerritoryId",
                table: "Cities",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1,
                column: "Hash",
                value: "$2a$13$taUT19JpztUix5wYAsKhBuMupc50hsjpUubPZDPrFrLytm23AWsma");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Territories_TerritoryId",
                table: "Cities",
                column: "TerritoryId",
                principalTable: "Territories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Territories_TerritoryId",
                table: "Cities");

            migrationBuilder.AlterColumn<int>(
                name: "TerritoryId",
                table: "Cities",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1,
                column: "Hash",
                value: "$2a$13$HKI/swgIfCWbK1d.4HNQVuPYgiLxsOb.T5DtTUGPj6OmFccKnxDbC");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Territories_TerritoryId",
                table: "Cities",
                column: "TerritoryId",
                principalTable: "Territories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
