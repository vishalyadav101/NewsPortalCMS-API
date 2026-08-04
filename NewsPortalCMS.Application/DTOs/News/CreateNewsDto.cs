using System.ComponentModel.DataAnnotations;

namespace NewsPortalCMS.DTOs.News
{
    public class CreateNewsDto
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(300)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public string? FeaturedImage { get; set; }

        public string? Author { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsPublished { get; set; }

        public int CategoryId { get; set; }
        public bool IsFeatured { get; set; }

    }
}