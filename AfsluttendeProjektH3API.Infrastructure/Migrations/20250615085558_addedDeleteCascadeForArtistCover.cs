using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AfsluttendeProjektH3API.Migrations
{
    /// <inheritdoc />
    public partial class addedDeleteCascadeForArtistCover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
        name: "FK_ArtistCover_Cover_CoverId",
        table: "ArtistCover");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistCover_Artist_ArtistId",
                table: "ArtistCover");

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistCover_Cover_CoverId",
                table: "ArtistCover",
                column: "CoverId",
                principalTable: "Cover",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistCover_Artist_ArtistId",
                table: "ArtistCover",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
