using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImportXml.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coords_HotelInfo_HotelInfoId",
                table: "Coords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelInfo",
                table: "HotelInfo");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "HotelInfo",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "HotelInfoId",
                table: "HotelInfo",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelInfo",
                table: "HotelInfo",
                column: "HotelInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coords_HotelInfo_HotelInfoId",
                table: "Coords",
                column: "HotelInfoId",
                principalTable: "HotelInfo",
                principalColumn: "HotelInfoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coords_HotelInfo_HotelInfoId",
                table: "Coords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelInfo",
                table: "HotelInfo");

            migrationBuilder.DropColumn(
                name: "HotelInfoId",
                table: "HotelInfo");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "HotelInfo",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelInfo",
                table: "HotelInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Coords_HotelInfo_HotelInfoId",
                table: "Coords",
                column: "HotelInfoId",
                principalTable: "HotelInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
