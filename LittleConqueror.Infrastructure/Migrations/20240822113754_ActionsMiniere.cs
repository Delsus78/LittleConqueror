using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ActionsMiniere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionsAgricoles");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Actions",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$ZtJQ0IgbLmQmAMH8G67f3emKyQWI1KbrseY2gbz1XoLfUXAjpxI82");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Actions");

            migrationBuilder.CreateTable(
                name: "ActionsAgricoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionsAgricoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionsAgricoles_Actions_Id",
                        column: x => x.Id,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$/qS7PwpJv0VX2Tl3Q3zfj.Ios0NVbuYDviv3D1sYujLLWBaPyKbv.");
        }
    }
}
