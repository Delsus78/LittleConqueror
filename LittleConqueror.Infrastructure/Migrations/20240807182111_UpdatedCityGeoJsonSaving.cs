using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCityGeoJsonSaving : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Geojson",
                table: "Cities",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1,
                column: "Hash",
                value: "$2a$13$pO3CFNoHZBWbDdXEeHO/TuU4BqY/PZUpplEw/pkLAH0Ky9g/XotVK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Geojson",
                table: "Cities");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1,
                column: "Hash",
                value: "$2a$13$Rp36yy.PiWho4KPs24Kc6.CONcgB8fB2DY.6j3defNtJVdQKw6rSq");
        }
    }
}
