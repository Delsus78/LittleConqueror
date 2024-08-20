using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ActionsEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ActionId",
                table: "Cities",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_Actions_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionsAgricoles",
                columns: table => new
                {
                    CityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionsAgricoles", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_ActionsAgricoles_Actions_CityId",
                        column: x => x.CityId,
                        principalTable: "Actions",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$Ck/xcNJxvZ7BUebWH.D5Gu3KHM99yCvU76qJcwYaFyu89Zpf4iluG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionsAgricoles");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "Cities");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$FWVeWDfCWuRqInM7ugLTEOBD0Uw7RTzr7dGm2I9M/i98f3r.8lJpy");
        }
    }
}
