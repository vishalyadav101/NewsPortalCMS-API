namespace NewsPortalCMS.DTOs.News
{
    public class NewsQueryDto
    {
        public string? Search { get; set; }

        public int? CategoryId { get; set; }

        public bool? IsPublished { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}