namespace NewsPortalCMS.Application.DTOs.Public
{
    public class PublicSubCategoryDto
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }
    }
}