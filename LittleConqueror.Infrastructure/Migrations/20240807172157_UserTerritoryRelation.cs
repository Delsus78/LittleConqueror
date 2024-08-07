using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserTerritoryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Territories_TerritoryId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TerritoryId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1,
                column: "Hash",
                value: "$2a$13$xlHeSItQVZ6FkODwIV/U/ePolLCvqap/897CHPd9aV5xAqbFyszuG");

            migrationBuilder.CreateIndex(
                name: "IX_Territories_OwnerId",
                table: "Territories",
                column: "OwnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Users_OwnerId",
                table: "Territories",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Users_OwnerId",
                table: "Territories");

            migrationBuilder.DropIndex(
                name: "IX_Territories_OwnerId",
                table: "Territories");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1,
                column: "Hash",
                value: "$2a$13$taUT19JpztUix5wYAsKhBuMupc50hsjpUubPZDPrFrLytm23AWsma");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TerritoryId",
                table: "Users",
                column: "TerritoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Territories_TerritoryId",
                table: "Users",
                column: "TerritoryId",
                principalTable: "Territories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
