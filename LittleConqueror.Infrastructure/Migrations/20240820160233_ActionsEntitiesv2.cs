using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LittleConqueror.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ActionsEntitiesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Cities_CityId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_ActionsAgricoles_Actions_CityId",
                table: "ActionsAgricoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actions",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "ActionId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Actions");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "ActionsAgricoles",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actions",
                table: "Actions",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$/qS7PwpJv0VX2Tl3Q3zfj.Ios0NVbuYDviv3D1sYujLLWBaPyKbv.");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Cities_Id",
                table: "Actions",
                column: "Id",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionsAgricoles_Actions_Id",
                table: "ActionsAgricoles",
                column: "Id",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Cities_Id",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_ActionsAgricoles_Actions_Id",
                table: "ActionsAgricoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actions",
                table: "Actions");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ActionsAgricoles",
                newName: "CityId");

            migrationBuilder.AddColumn<long>(
                name: "ActionId",
                table: "Cities",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "Actions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actions",
                table: "Actions",
                column: "CityId");

            migrationBuilder.UpdateData(
                table: "AuthUsers",
                keyColumn: "Id",
                keyValue: -1L,
                column: "Hash",
                value: "$2a$13$Ck/xcNJxvZ7BUebWH.D5Gu3KHM99yCvU76qJcwYaFyu89Zpf4iluG");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Cities_CityId",
                table: "Actions",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActionsAgricoles_Actions_CityId",
                table: "ActionsAgricoles",
                column: "CityId",
                principalTable: "Actions",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
