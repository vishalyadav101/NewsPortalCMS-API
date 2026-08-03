namespace NewsPortalCMS.Application.DTOs.Public
{
    public class PublicNewsDetailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string? FeaturedImage { get; set; }

        public string? Author { get; set; }

        public DateTime PublishDate { get; set; }

        public int ViewCount { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public List<string> Tags { get; set; } = new();

        public List<PublicCommentDto> Comments { get; set; } = new();
    }
}