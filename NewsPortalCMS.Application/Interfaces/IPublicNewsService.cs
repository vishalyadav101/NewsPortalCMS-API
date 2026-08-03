using NewsPortalCMS.Application.DTOs.Public;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface IPublicNewsService
    {
        Task<IEnumerable<PublicNewsDto>> GetLatestNewsAsync(int count);

        Task<PublicNewsDetailsDto?> GetNewsBySlugAsync(string slug);

        Task<IEnumerable<PublicNewsDto>> GetNewsByCategoryAsync(int categoryId);

        Task<IEnumerable<PublicNewsDto>> SearchNewsAsync(string keyword);

        Task<IEnumerable<PublicNewsDto>> GetPopularNewsAsync(int count);
    }
}