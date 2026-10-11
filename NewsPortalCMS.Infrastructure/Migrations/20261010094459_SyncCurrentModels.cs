using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsPortalCMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SyncCurrentModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Categories_CategoryId",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "WebsiteSettings",
                newName: "WhatsAppNumber");

            migrationBuilder.AddColumn<string>(
                name: "AdvertisingEmail",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CanonicalUrl",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CopyrightText",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DefaultLanguage",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultNewsImageMediaId",
                table: "WebsiteSettings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultSocialImageMediaId",
                table: "WebsiteSettings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditorialEmail",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsUrl",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleSiteVerification",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficeAddress",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationName",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryColor",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryColor",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelegramUrl",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TimeZone",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedById",
                table: "WebsiteSettings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteTagline",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatsAppChannelUrl",
                table: "WebsiteSettings",
                type: "nvarchar(max)",
                nullable: true);

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
                name: "AdvertisingEmail",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "CanonicalUrl",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "CopyrightText",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "DefaultLanguage",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "DefaultNewsImageMediaId",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "DefaultSocialImageMediaId",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "EditorialEmail",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "GoogleMapsUrl",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "GoogleSiteVerification",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "OfficeAddress",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "OrganizationName",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "PrimaryColor",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "SecondaryColor",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "TelegramUrl",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "TimeZone",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "WebsiteTagline",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "WhatsAppChannelUrl",
                table: "WebsiteSettings");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "WhatsAppNumber",
                table: "WebsiteSettings",
                newName: "Address");

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
