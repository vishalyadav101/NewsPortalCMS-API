using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    // DbContext DI se milega
    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Saari categories database se laayega
    public async Task<List<Category>> GetAllAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();
    }

    // Id ke basis par single category laayega
    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    // New category database me insert karega
    public async Task<Category> CreateAsync(Category category)
    {
        await _context.Categories.AddAsync(category);

        await _context.SaveChangesAsync();

        return category;
    }

    // Existing category ke changes save karega
    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);

        await _context.SaveChangesAsync();
    }

    // Category delete karega
    public async Task DeleteAsync(Category category)
    {
        _context.Categories.Remove(category);

        await _context.SaveChangesAsync();
    }

    // Create ke time duplicate slug check
    public async Task<bool> SlugExistsAsync(string slug)
    {
        return await _context.Categories
            .AnyAsync(x => x.Slug == slug);
    }

    // Update ke time current category ko exclude karke
    // duplicate slug check
    public async Task<bool> SlugExistsAsync(
        string slug,
        int excludeCategoryId)
    {
        return await _context.Categories
            .AnyAsync(x =>
                x.Slug == slug &&
                x.Id != excludeCategoryId);
    }
}