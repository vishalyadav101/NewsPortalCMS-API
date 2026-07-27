using NewsPortalCMS.Application.DTOs.SubCategory;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Infrastructure.Services;

public class SubCategoryService : ISubCategoryService
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly ICategoryRepository _categoryRepository;

    public SubCategoryService(
        ISubCategoryRepository subCategoryRepository,
        ICategoryRepository categoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
        _categoryRepository = categoryRepository;
    }

    // Get All
    public async Task<List<SubCategoryResponseDto>> GetAllAsync()
    {
        var subCategories =
            await _subCategoryRepository.GetAllAsync();

        return subCategories.Select(x => new SubCategoryResponseDto
        {
            Id = x.Id,
            CategoryId = x.CategoryId,
            CategoryName = x.Category.Name,
            Name = x.Name,
            Slug = x.Slug,
            Description = x.Description,
            IsActive = x.IsActive,
            DisplayOrder = x.DisplayOrder,
            CreatedDate = x.CreatedDate,
            UpdatedDate = x.UpdatedDate
        }).ToList();
    }

    // Get By Id
    public async Task<SubCategoryResponseDto?> GetByIdAsync(int id)
    {
        var subCategory =
            await _subCategoryRepository.GetByIdAsync(id);

        if (subCategory == null)
        {
            return null;
        }

        return new SubCategoryResponseDto
        {
            Id = subCategory.Id,
            CategoryId = subCategory.CategoryId,
            CategoryName = subCategory.Category.Name,
            Name = subCategory.Name,
            Slug = subCategory.Slug,
            Description = subCategory.Description,
            IsActive = subCategory.IsActive,
            DisplayOrder = subCategory.DisplayOrder,
            CreatedDate = subCategory.CreatedDate,
            UpdatedDate = subCategory.UpdatedDate
        };
    }

    // Create
    public async Task<SubCategoryResponseDto> CreateAsync(
        SubCategoryCreateDto model)
    {
        // Parent Category exist karti hai?
        var category =
            await _categoryRepository.GetByIdAsync(model.CategoryId);

        if (category == null)
        {
            throw new InvalidOperationException(
                "Category not found.");
        }

        var slug = model.Slug.Trim().ToLowerInvariant();

        // Same Category ke andar duplicate slug check
        var slugExists =
            await _subCategoryRepository.SlugExistsAsync(
                model.CategoryId,
                slug);

        if (slugExists)
        {
            throw new InvalidOperationException(
                "SubCategory with this slug already exists in this category.");
        }

        var subCategory = new SubCategory
        {
            CategoryId = model.CategoryId,
            Name = model.Name.Trim(),
            Slug = slug,
            Description = model.Description?.Trim(),
            IsActive = model.IsActive,
            DisplayOrder = model.DisplayOrder,
            CreatedDate = DateTime.UtcNow
        };

        var created =
            await _subCategoryRepository.CreateAsync(subCategory);

        return new SubCategoryResponseDto
        {
            Id = created.Id,
            CategoryId = created.CategoryId,

            // Category already upar fetch ki thi
            CategoryName = category.Name,

            Name = created.Name,
            Slug = created.Slug,
            Description = created.Description,
            IsActive = created.IsActive,
            DisplayOrder = created.DisplayOrder,
            CreatedDate = created.CreatedDate,
            UpdatedDate = created.UpdatedDate
        };
    }

    // Update
    public async Task<bool> UpdateAsync(
        int id,
        SubCategoryUpdateDto model)
    {
        var subCategory =
            await _subCategoryRepository.GetByIdAsync(id);

        if (subCategory == null)
        {
            return false;
        }

        // New/selected Category exist karti hai?
        var category =
            await _categoryRepository.GetByIdAsync(model.CategoryId);

        if (category == null)
        {
            throw new InvalidOperationException(
                "Category not found.");
        }

        var slug = model.Slug.Trim().ToLowerInvariant();

        // Current record ko exclude karke duplicate check
        var slugExists =
            await _subCategoryRepository.SlugExistsAsync(
                model.CategoryId,
                slug,
                id);

        if (slugExists)
        {
            throw new InvalidOperationException(
                "SubCategory with this slug already exists in this category.");
        }

        subCategory.CategoryId = model.CategoryId;
        subCategory.Name = model.Name.Trim();
        subCategory.Slug = slug;
        subCategory.Description = model.Description?.Trim();
        subCategory.IsActive = model.IsActive;
        subCategory.DisplayOrder = model.DisplayOrder;
        subCategory.UpdatedDate = DateTime.UtcNow;

        await _subCategoryRepository.UpdateAsync(subCategory);

        return true;
    }

    // Delete
    public async Task<bool> DeleteAsync(int id)
    {
        var subCategory =
            await _subCategoryRepository.GetByIdAsync(id);

        if (subCategory == null)
        {
            return false;
        }

        await _subCategoryRepository.DeleteAsync(subCategory);

        return true;
    }
}