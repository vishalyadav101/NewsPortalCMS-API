using NewsPortalCMS.Application.DTOs.Public;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface IPublicCategoryService
    {
        Task<IEnumerable<PublicCategoryDto>> GetActiveCategoriesAsync();

        Task<PublicCategoryDto?> GetCategoryBySlugAsync(string slug);
    }
}