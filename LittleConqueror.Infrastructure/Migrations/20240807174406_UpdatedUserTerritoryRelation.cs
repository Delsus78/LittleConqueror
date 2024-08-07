using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserTerritoryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TerritoryId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1,
                column: "Hash",
                value: "$2a$13$Rp36yy.PiWho4KPs24Kc6.CONcgB8fB2DY.6j3defNtJVdQKw6rSq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TerritoryId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1,
                column: "Hash",
                value: "$2a$13$xlHeSItQVZ6FkODwIV/U/ePolLCvqap/897CHPd9aV5xAqbFyszuG");
        }
    }
}
