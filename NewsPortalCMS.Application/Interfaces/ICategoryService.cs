using NewsPortalCMS.Application.DTOs.Category;

namespace NewsPortalCMS.Application.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryResponseDto>> GetAllAsync();

    Task<CategoryResponseDto?> GetByIdAsync(int id);

    Task<CategoryResponseDto> CreateAsync(CategoryCreateDto model);

    Task<bool> UpdateAsync(int id, CategoryUpdateDto model);

    Task<bool> DeleteAsync(int id);
}