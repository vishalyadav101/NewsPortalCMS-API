using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsPortalCMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsPerformanceIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_News_IsDeleted",
                table: "News",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_News_IsFeatured",
                table: "News",
                column: "IsFeatured");

            migrationBuilder.CreateIndex(
                name: "IX_News_IsPublished",
                table: "News",
                column: "IsPublished");

            migrationBuilder.CreateIndex(
                name: "IX_News_PublishDate",
                table: "News",
                column: "PublishDate");

            migrationBuilder.CreateIndex(
                name: "IX_News_Slug",
                table: "News",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_News_ViewCount",
                table: "News",
                column: "ViewCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_News_IsDeleted",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_IsFeatured",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_IsPublished",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_PublishDate",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_Slug",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_ViewCount",
                table: "News");
        }
    }
}
