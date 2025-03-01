using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImportXml.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferItems_Offers_OffersId",
                table: "OfferItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferItems",
                table: "OfferItems");

            migrationBuilder.RenameTable(
                name: "OfferItems",
                newName: "Offer");

            migrationBuilder.RenameIndex(
                name: "IX_OfferItems_OffersId",
                table: "Offer",
                newName: "IX_Offer_OffersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offer",
                table: "Offer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_Offers_OffersId",
                table: "Offer",
                column: "OffersId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_Offers_OffersId",
                table: "Offer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offer",
                table: "Offer");

            migrationBuilder.RenameTable(
                name: "Offer",
                newName: "OfferItems");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_OffersId",
                table: "OfferItems",
                newName: "IX_OfferItems_OffersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferItems",
                table: "OfferItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferItems_Offers_OffersId",
                table: "OfferItems",
                column: "OffersId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
