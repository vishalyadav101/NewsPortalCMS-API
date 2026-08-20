namespace NewsPortalCMS.DTOs.News
{
    public class NewsDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string? FeaturedImage { get; set; }

        public string? FeaturedVideo { get; set; }

        public string Author { get; set; } = string.Empty;

        public DateTime PublishDate { get; set; }

        public bool IsPublished { get; set; }

        public bool IsFeatured { get; set; }

        public int ViewCount { get; set; }

        // ==========================================
        // CATEGORY
        // ==========================================

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        // ==========================================
        // SUB CATEGORY
        // ==========================================

        public int? SubCategoryId { get; set; }

        public string SubCategoryName { get; set; } = string.Empty;

        // ==========================================
        // AUDIT
        // ==========================================

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}