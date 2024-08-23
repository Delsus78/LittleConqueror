using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ActionsMinierev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ActionsDiplomatiques",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionsDiplomatiques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionsDiplomatiques_Actions_Id",
                        column: x => x.Id,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionsEspionnages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionsEspionnages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionsEspionnages_Actions_Id",
                        column: x => x.Id,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionsMilitaires",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionsMilitaires", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionsMilitaires_Actions_Id",
                        column: x => x.Id,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionsMiniere",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionsMiniere", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionsMiniere_Actions_Id",
                        column: x => x.Id,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionsTechnologiques",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionsTechnologiques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionsTechnologiques_Actions_Id",
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
                value: "$2a$13$6SNYT4gei59EQHI0v2v7zuhw1E1VkIZcjYIMuf.nuMLEKUtpU2n8K");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionsAgricoles");

            migrationBuilder.DropTable(
                name: "ActionsDiplomatiques");

            migrationBuilder.DropTable(
                name: "ActionsEspionnages");

            migrationBuilder.DropTable(
                name: "ActionsMilitaires");

            migrationBuilder.DropTable(
                name: "ActionsMiniere");

            migrationBuilder.DropTable(
                name: "ActionsTechnologiques");

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
    }
}
