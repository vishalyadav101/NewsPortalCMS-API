using NewsPortalCMS.Entities;
using NewsPortalCMS.DTOs.News;

namespace NewsPortalCMS.Interfaces
{
    public interface INewsRepository
    {
        Task<(IEnumerable<News> Items, int TotalCount)> GetAllAsync(
     NewsQueryRequest request);
        Task<News?> GetByIdAsync(int id);

        Task<News> CreateAsync(News news);

        Task<News?> UpdateAsync(News news);

        Task<bool> DeleteAsync(int id);

        Task<bool> RestoreAsync(int id);

        Task<bool> TitleExistsAsync(string title);

        Task<bool> SlugExistsAsync(string slug);

        Task<bool> CategoryExistsAsync(int categoryId);

        Task<bool> ExistsAsync(int id);
    }
}