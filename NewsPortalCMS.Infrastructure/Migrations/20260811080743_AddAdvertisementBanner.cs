using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsPortalCMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdvertisementBanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Media_MediaId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_MediaId",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "Advertisements");

            migrationBuilder.AddColumn<string>(
                name: "BannerUrl",
                table: "Advertisements",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerUrl",
                table: "Advertisements");

            migrationBuilder.AddColumn<int>(
                name: "MediaId",
                table: "Advertisements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_MediaId",
                table: "Advertisements",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Media_MediaId",
                table: "Advertisements",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
