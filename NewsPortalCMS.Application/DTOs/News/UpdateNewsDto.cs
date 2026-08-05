using System.ComponentModel.DataAnnotations;

namespace NewsPortalCMS.DTOs.News
{
    public class UpdateNewsDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Slug { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        // File path only
        public string FeaturedImage { get; set; }
        public string? FeaturedVideo { get; set; }

        public string Author { get; set; } = string.Empty;

        public DateTime PublishDate { get; set; }

        public bool IsPublished { get; set; }

        public bool IsFeatured { get; set; }

        public int CategoryId { get; set; }
    }
}