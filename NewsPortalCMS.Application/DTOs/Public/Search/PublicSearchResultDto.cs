namespace NewsPortalCMS.Application.DTOs.Public.Search
{
    public class PublicSearchResultDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string? FeaturedImage { get; set; }

        public string? Author { get; set; }

        public DateTime PublishDate { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }
}