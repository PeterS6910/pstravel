using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImportXml.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_Offers_OffersId1",
                table: "Offer");

            migrationBuilder.DropIndex(
                name: "IX_Offer_OffersId1",
                table: "Offer");

            migrationBuilder.DropColumn(
                name: "OffersId1",
                table: "Offer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OffersId1",
                table: "Offer",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_OffersId1",
                table: "Offer",
                column: "OffersId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_Offers_OffersId1",
                table: "Offer",
                column: "OffersId1",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
