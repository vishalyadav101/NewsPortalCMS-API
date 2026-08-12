using NewsPortalCMS.Entities;

namespace NewsPortalCMS.Interfaces
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllAsync();

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