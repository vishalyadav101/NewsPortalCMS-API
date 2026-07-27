using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces;

public interface ISubCategoryRepository
{
    Task<List<SubCategory>> GetAllAsync();

    Task<SubCategory?> GetByIdAsync(int id);

    Task<SubCategory> CreateAsync(SubCategory subCategory);

    Task UpdateAsync(SubCategory subCategory);

    Task DeleteAsync(SubCategory subCategory);

    Task<bool> SlugExistsAsync(int categoryId, string slug);

    Task<bool> SlugExistsAsync(
        int categoryId,
        string slug,
        int excludeSubCategoryId);
}