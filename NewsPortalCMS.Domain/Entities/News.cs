using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Entities
{
    public class News
    {
        [Key]
        public int Id { get; set; }

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

        [MaxLength(500)]
        public string? FeaturedImage { get; set; }

        [MaxLength(100)]
        public string? Author { get; set; }

        public DateTime PublishDate { get; set; }

        public bool IsPublished { get; set; }

        public int ViewCount { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
        public ICollection<NewsTag> NewsTags { get; set; } = new List<NewsTag>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public bool IsFeatured { get; set; } = false;
    }
}