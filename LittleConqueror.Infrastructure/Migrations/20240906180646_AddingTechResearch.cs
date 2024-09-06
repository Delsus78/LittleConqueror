using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingTechResearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResearchPoints",
                table: "Resources");

            migrationBuilder.CreateTable(
                name: "TechResearches",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ResearchDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ResearchCategory = table.Column<int>(type: "integer", nullable: false),
                    ResearchType = table.Column<int>(type: "integer", nullable: false),
                    ResearchStatus = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechResearches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechResearches_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$qBt3Mz.HhC8HqFn9aV/kh.0utpUWTmpHq9Rh.Wv68PWiyJFB3WUmK");

            migrationBuilder.CreateIndex(
                name: "IX_TechResearches_UserId_ResearchCategory",
                table: "TechResearches",
                columns: new[] { "UserId", "ResearchCategory" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechResearches");

            migrationBuilder.AddColumn<int>(
                name: "ResearchPoints",
                table: "Resources",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$08cT9NwqZs3sA6QPnWjDNecIXvHfkqXBnBSRZdiagMPSo8JEq1yPq");
        }
    }
}
