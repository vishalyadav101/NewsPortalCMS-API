using System.ComponentModel.DataAnnotations;

namespace NewsPortalCMS.Application.DTOs.Seo
{
    public class CreateSeoDto
    {
        [Required]
        [MaxLength(200)]
        public string PageName { get; set; } = string.Empty;

        [Required]
        [MaxLength(250)]
        public string MetaTitle { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? MetaDescription { get; set; }

        [MaxLength(500)]
        public string? MetaKeywords { get; set; }

        [MaxLength(250)]
        public string? CanonicalUrl { get; set; }

        [MaxLength(100)]
        public string? Robots { get; set; }

        [MaxLength(250)]
        public string? OgTitle { get; set; }

        [MaxLength(500)]
        public string? OgDescription { get; set; }

        [MaxLength(250)]
        public string? OgImage { get; set; }

        [MaxLength(250)]
        public string? TwitterTitle { get; set; }

        [MaxLength(500)]
        public string? TwitterDescription { get; set; }

        [MaxLength(250)]
        public string? TwitterImage { get; set; }

        public string? SchemaMarkup { get; set; }

        public bool IsActive { get; set; } = true;
    }
}