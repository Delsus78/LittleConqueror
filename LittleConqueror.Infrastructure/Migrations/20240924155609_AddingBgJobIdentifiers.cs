using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingBgJobIdentifiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BackgroundJobIdentifiers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobIdentifier = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackgroundJobIdentifiers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$T7FQJA54xpvMOOQpn0HkjOw6EIjvrEweLUIB3UXleSlqOccboYEaa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackgroundJobIdentifiers");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$gUWfGiPXnSXV1H8C7KyzpO.npk29c64PAtnUPD49XS0dIENHKarvq");
        }
    }
}
