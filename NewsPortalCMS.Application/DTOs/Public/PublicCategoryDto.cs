namespace NewsPortalCMS.Application.DTOs.Public
{
    public class PublicCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int DisplayOrder { get; set; }
    }
}