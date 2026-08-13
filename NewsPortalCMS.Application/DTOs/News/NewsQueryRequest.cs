using NewsPortalCMS.Application.Common.Pagination;

namespace NewsPortalCMS.DTOs.News
{
    public class NewsQueryRequest : PaginationRequest
    {
        public string? Search { get; set; }

        public int? CategoryId { get; set; }

        public bool? IsPublished { get; set; }

        public bool? IsFeatured { get; set; }

        public string? SortBy { get; set; }
    }
}