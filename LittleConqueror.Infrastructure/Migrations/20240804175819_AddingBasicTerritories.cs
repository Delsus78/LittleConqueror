using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingBasicTerritories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TerritoryEntityId",
                table: "Cities",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Territories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Territories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Territories_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_TerritoryEntityId",
                table: "Cities",
                column: "TerritoryEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Territories_OwnerId",
                table: "Territories",
                column: "OwnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Territories_TerritoryEntityId",
                table: "Cities",
                column: "TerritoryEntityId",
                principalTable: "Territories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Territories_TerritoryEntityId",
                table: "Cities");

            migrationBuilder.DropTable(
                name: "Territories");

            migrationBuilder.DropIndex(
                name: "IX_Cities_TerritoryEntityId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "TerritoryEntityId",
                table: "Cities");
        }
    }
}
