using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsPortalCMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeoModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Seos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MetaDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MetaKeywords = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CanonicalUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Robots = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OgTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OgDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    OgImage = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TwitterTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TwitterDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TwitterImage = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SchemaMarkup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seos");
        }
    }
}
