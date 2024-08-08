using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqueUserIdForAuthUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1,
                column: "Hash",
                value: "$2a$13$XePFdIPxMQ.XYe0u8QM2EefJ.kDexprmR77HD6jeOwFpHLQ2SI/Ri");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1,
                column: "Hash",
                value: "$2a$13$pO3CFNoHZBWbDdXEeHO/TuU4BqY/PZUpplEw/pkLAH0Ky9g/XotVK");
        }
    }
}
