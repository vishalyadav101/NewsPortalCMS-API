namespace NewsPortalCMS.Application.DTOs.Public.Seo
{
    public class PublicSeoDto
    {
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
    }
}