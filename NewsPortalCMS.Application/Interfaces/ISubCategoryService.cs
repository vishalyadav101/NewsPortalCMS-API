using NewsPortalCMS.Application.DTOs.SubCategory;

namespace NewsPortalCMS.Application.Interfaces;

public interface ISubCategoryService
{
    Task<List<SubCategoryResponseDto>> GetAllAsync();

    Task<SubCategoryResponseDto?> GetByIdAsync(int id);

    Task<SubCategoryResponseDto> CreateAsync(
        SubCategoryCreateDto model);

    Task<bool> UpdateAsync(
        int id,
        SubCategoryUpdateDto model);

    Task<bool> DeleteAsync(int id);
}