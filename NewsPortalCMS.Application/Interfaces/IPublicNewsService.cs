using NewsPortalCMS.Application.DTOs.Public;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface IPublicNewsService
    {
        Task<IEnumerable<PublicNewsDto>> GetLatestNewsAsync(int count);

        Task<IEnumerable<PublicNewsDto>> GetFeaturedNewsAsync(int count);

        Task<IEnumerable<PublicNewsDto>> GetPopularNewsAsync(int count);

        Task<IEnumerable<PublicNewsDto>> GetNewsByCategoryAsync(int categoryId);

        Task<IEnumerable<PublicNewsDto>> GetNewsBySubcategoryAsync(int subcategoryId);

        Task<IEnumerable<PublicNewsDto>> SearchNewsAsync(string keyword);

        Task<PublicNewsDetailsDto?> GetNewsBySlugAsync(string slug);
    }
}