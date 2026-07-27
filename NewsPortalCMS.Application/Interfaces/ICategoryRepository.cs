using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();

    Task<Category?> GetByIdAsync(int id);

    Task<Category> CreateAsync(Category category);

    Task UpdateAsync(Category category);

    Task DeleteAsync(Category category);

    Task<bool> SlugExistsAsync(string slug);

    Task<bool> SlugExistsAsync(string slug, int excludeCategoryId);
}