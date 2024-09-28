using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixResearchTypeUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TechResearches_UserId_ResearchCategory",
                table: "TechResearches");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$LTSgQKByHPhK7KG2WZgBIeelE.kuddR4uguTJ9u7keWZ3gfJVu1DK");

            migrationBuilder.CreateIndex(
                name: "IX_TechResearches_UserId_ResearchType",
                table: "TechResearches",
                columns: new[] { "UserId", "ResearchType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TechResearches_UserId_ResearchType",
                table: "TechResearches");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$T7FQJA54xpvMOOQpn0HkjOw6EIjvrEweLUIB3UXleSlqOccboYEaa");

            migrationBuilder.CreateIndex(
                name: "IX_TechResearches_UserId_ResearchCategory",
                table: "TechResearches",
                columns: new[] { "UserId", "ResearchCategory" },
                unique: true);
        }
    }
}
