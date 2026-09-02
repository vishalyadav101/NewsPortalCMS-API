using NewsPortalCMS.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories
{
    public interface IPublicNewsRepository
    {
        Task<IEnumerable<News>> GetLatestNewsAsync(int count);

        Task<IEnumerable<News>> GetFeaturedNewsAsync(int count);

        Task<News?> GetNewsBySlugAsync(string slug);

        Task<IEnumerable<News>> GetNewsByCategoryAsync(int categoryId);

        Task<IEnumerable<News>> GetNewsBySubcategoryAsync(int subcategoryId);

        Task<IEnumerable<News>> SearchNewsAsync(string keyword);

        Task<IEnumerable<News>> GetPopularNewsAsync(int count);
    }
}