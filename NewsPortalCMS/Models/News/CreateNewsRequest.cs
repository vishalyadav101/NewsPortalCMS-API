using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NewsPortalCMS.Models.News
{
    public class CreateNewsRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Slug { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public IFormFile? FeaturedImage { get; set; }

        public IFormFile? FeaturedVideo { get; set; }

        public string Author { get; set; } = string.Empty;

        public DateTime PublishDate { get; set; }

        public bool IsPublished { get; set; }

        public bool IsFeatured { get; set; }

        public int CategoryId { get; set; }

        // ==========================================
        // SUB CATEGORY
        // Optional for existing records
        // ==========================================

        public int? SubCategoryId { get; set; }
    }
}