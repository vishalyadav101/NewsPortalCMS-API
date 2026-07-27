using NewsPortalCMS.Application.DTOs.Category;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    // Repository Dependency Injection se milegi
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    // Saari categories get karega
    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return categories.Select(category => new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Description = category.Description,
            IsActive = category.IsActive,
            DisplayOrder = category.DisplayOrder,
            CreatedDate = category.CreatedDate,
            UpdatedDate = category.UpdatedDate
        }).ToList();
    }

    // Id se single category get karega
    public async Task<CategoryResponseDto?> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            return null;
        }

        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Description = category.Description,
            IsActive = category.IsActive,
            DisplayOrder = category.DisplayOrder,
            CreatedDate = category.CreatedDate,
            UpdatedDate = category.UpdatedDate
        };
    }

    // New category create karega
    public async Task<CategoryResponseDto> CreateAsync(CategoryCreateDto model)
    {
        // Slug ko normalize karo
        var slug = model.Slug.Trim().ToLowerInvariant();

        // Duplicate slug check
        var slugExists = await _categoryRepository.SlugExistsAsync(slug);

        if (slugExists)
        {
            throw new InvalidOperationException(
                "Category with this slug already exists.");
        }

        var category = new Category
        {
            Name = model.Name.Trim(),
            Slug = slug,
            Description = model.Description?.Trim(),
            IsActive = model.IsActive,
            DisplayOrder = model.DisplayOrder,
            CreatedDate = DateTime.UtcNow
        };

        var createdCategory =
            await _categoryRepository.CreateAsync(category);

        return new CategoryResponseDto
        {
            Id = createdCategory.Id,
            Name = createdCategory.Name,
            Slug = createdCategory.Slug,
            Description = createdCategory.Description,
            IsActive = createdCategory.IsActive,
            DisplayOrder = createdCategory.DisplayOrder,
            CreatedDate = createdCategory.CreatedDate,
            UpdatedDate = createdCategory.UpdatedDate
        };
    }

    // Existing category update karega
    public async Task<bool> UpdateAsync(
        int id,
        CategoryUpdateDto model)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            return false;
        }

        var slug = model.Slug.Trim().ToLowerInvariant();

        // Current category ko exclude karke duplicate check
        var slugExists =
            await _categoryRepository.SlugExistsAsync(slug, id);

        if (slugExists)
        {
            throw new InvalidOperationException(
                "Category with this slug already exists.");
        }

        category.Name = model.Name.Trim();
        category.Slug = slug;
        category.Description = model.Description?.Trim();
        category.IsActive = model.IsActive;
        category.DisplayOrder = model.DisplayOrder;
        category.UpdatedDate = DateTime.UtcNow;

        await _categoryRepository.UpdateAsync(category);

        return true;
    }

    // Category delete karega
    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            return false;
        }

        await _categoryRepository.DeleteAsync(category);

        return true;
    }
}