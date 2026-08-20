namespace NewsPortalCMS.Domain.Entities
{
    public class SubCategory
    {
        public int Id { get; set; }

        // Parent Category
        public int CategoryId { get; set; }

        // SubCategory details
        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        // Navigation Property
        public Category Category { get; set; } = null!;
    }
}