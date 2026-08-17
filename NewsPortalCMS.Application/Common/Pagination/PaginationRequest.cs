namespace NewsPortalCMS.Application.Common.Pagination
{
    public class PaginationRequest
    {
        private int? _pageNumber;
        private int? _pageSize;

        public int? PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value.HasValue
                ? (value.Value < 1 ? 1 : value.Value)
                : null;
        }

        public int? PageSize
        {
            get => _pageSize;
            set => _pageSize = value.HasValue
                ? value.Value switch
                {
                    < 1 => 10,
                    > 100 => 100,
                    _ => value.Value
                }
                : null;
        }
    }
}