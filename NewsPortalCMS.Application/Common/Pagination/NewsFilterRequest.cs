namespace NewsPortalCMS.Application.Common.Pagination
{
    public class NewsFilterRequest
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public int? CategoryId { get; set; }

        public bool? IsPublished { get; set; }

        public bool? IsFeatured { get; set; }

        public string? Search { get; set; }

        public string SortBy { get; set; } = "latest";
    }
}