using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories;

public class SubCategoryRepository : ISubCategoryRepository
{
    private readonly ApplicationDbContext _context;

    public SubCategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Saari SubCategories Category ke saath return karega
    public async Task<List<SubCategory>> GetAllAsync()
    {
        return await _context.SubCategories
            .AsNoTracking()
            .Include(x => x.Category)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();
    }

    // Id se single SubCategory
    public async Task<SubCategory?> GetByIdAsync(int id)
    {
        return await _context.SubCategories
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    // Create
    public async Task<SubCategory> CreateAsync(
        SubCategory subCategory)
    {
        await _context.SubCategories.AddAsync(subCategory);

        await _context.SaveChangesAsync();

        return subCategory;
    }

    // Update
    public async Task UpdateAsync(SubCategory subCategory)
    {
        _context.SubCategories.Update(subCategory);

        await _context.SaveChangesAsync();
    }

    // Delete
    public async Task DeleteAsync(SubCategory subCategory)
    {
        _context.SubCategories.Remove(subCategory);

        await _context.SaveChangesAsync();
    }

    // Create ke time duplicate check
    public async Task<bool> SlugExistsAsync(
        int categoryId,
        string slug)
    {
        return await _context.SubCategories
            .AnyAsync(x =>
                x.CategoryId == categoryId &&
                x.Slug == slug);
    }

    // Update ke time current SubCategory ko exclude karo
    public async Task<bool> SlugExistsAsync(
        int categoryId,
        string slug,
        int excludeSubCategoryId)
    {
        return await _context.SubCategories
            .AnyAsync(x =>
                x.CategoryId == categoryId &&
                x.Slug == slug &&
                x.Id != excludeSubCategoryId);
    }
}