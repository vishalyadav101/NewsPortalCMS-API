namespace NewsPortalCMS.Application.DTOs.Public.Search
{
    public class PagedResultDto<T>
    {
        public int TotalRecords { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<T> Data { get; set; } = new List<T>();
    }
}