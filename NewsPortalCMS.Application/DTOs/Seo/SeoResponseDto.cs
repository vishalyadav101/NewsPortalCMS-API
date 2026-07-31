namespace NewsPortalCMS.Application.DTOs.Seo
{
    public class SeoResponseDto
    {
        public int Id { get; set; }

        public string PageName { get; set; } = string.Empty;

        public string MetaTitle { get; set; } = string.Empty;

        public string? MetaDescription { get; set; }

        public string? MetaKeywords { get; set; }

        public string? CanonicalUrl { get; set; }

        public string? Robots { get; set; }

        public string? OgTitle { get; set; }

        public string? OgDescription { get; set; }

        public string? OgImage { get; set; }

        public string? TwitterTitle { get; set; }

        public string? TwitterDescription { get; set; }

        public string? TwitterImage { get; set; }

        public string? SchemaMarkup { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}