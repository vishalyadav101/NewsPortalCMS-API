using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsPortalCMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSubCategoryToNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News");

            migrationBuilder.AlterColumn<string>(
                name: "FeaturedVideo",
                table: "News",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "News",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_SubCategoryId",
                table: "News",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_SubCategories_SubCategoryId",
                table: "News",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_News_SubCategories_SubCategoryId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_SubCategoryId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "News");

            migrationBuilder.AlterColumn<string>(
                name: "FeaturedVideo",
                table: "News",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
