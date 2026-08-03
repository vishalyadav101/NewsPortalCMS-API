using NewsPortalCMS.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories
{
    public interface IPublicSearchRepository
    {
        Task<(IEnumerable<News> News, int TotalCount)> SearchNewsAsync(
            string keyword,
            int pageNumber,
            int pageSize);
    }
}