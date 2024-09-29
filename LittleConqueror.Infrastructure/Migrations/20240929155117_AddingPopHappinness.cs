using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingPopHappinness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PopHappinessConfigs",
                table: "Configs",
                type: "jsonb",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$kkrnfBCW1uiRtJG7XTK.GeMGP7sbREzkkVtGhnM0NpUrIV1/fsezK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PopHappinessConfigs",
                table: "Configs");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$LTSgQKByHPhK7KG2WZgBIeelE.kuddR4uguTJ9u7keWZ3gfJVu1DK");
        }
    }
}
