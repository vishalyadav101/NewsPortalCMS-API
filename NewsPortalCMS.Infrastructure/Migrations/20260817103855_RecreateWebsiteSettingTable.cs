using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsPortalCMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RecreateWebsiteSettingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebsiteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation(
                            "SqlServer:Identity",
                            "1, 1"),

                    // General Information
                    WebsiteName = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false),

                    WebsiteTagline = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    OrganizationName = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    WebsiteUrl = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    WebsiteDescription = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    DefaultLanguage = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    TimeZone = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    CopyrightText = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    // Media URLs
                    LogoUrl = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    FaviconUrl = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    // Media Library References
                    DefaultNewsImageMediaId = table.Column<int>(
                        type: "int",
                        nullable: true),

                    DefaultSocialImageMediaId = table.Column<int>(
                        type: "int",
                        nullable: true),

                    // Website Branding
                    PrimaryColor = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    SecondaryColor = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    // Contact Information
                    ContactEmail = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    EditorialEmail = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    AdvertisingEmail = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    ContactPhone = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    WhatsAppNumber = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    OfficeAddress = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    GoogleMapsUrl = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    // Social Media
                    FacebookUrl = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    InstagramUrl = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    YouTubeUrl = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    TwitterUrl = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    LinkedInUrl = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    TelegramUrl = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    WhatsAppChannelUrl = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    // SEO
                    MetaTitle = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    MetaDescription = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    MetaKeywords = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    CanonicalUrl = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    GoogleSiteVerification = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    // Footer
                    FooterText = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: true),

                    // Audit Information
                    CreatedDate = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: false),

                    UpdatedDate = table.Column<DateTime>(
                        type: "datetime2",
                        nullable: true),

                    UpdatedById = table.Column<int>(
                        type: "int",
                        nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_WebsiteSettings",
                        x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebsiteSettings");
        }
    }
}