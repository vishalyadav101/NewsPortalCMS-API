using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories;

public class TagRepository : ITagRepository
{
    private readonly ApplicationDbContext _context;

    public TagRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Saare tags
    public async Task<List<Tag>> GetAllAsync()
    {
        return await _context.Tags
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    // Id se tag
    public async Task<Tag?> GetByIdAsync(int id)
    {
        return await _context.Tags
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    // Create
    public async Task<Tag> CreateAsync(Tag tag)
    {
        await _context.Tags.AddAsync(tag);

        await _context.SaveChangesAsync();

        return tag;
    }

    // Update
    public async Task UpdateAsync(Tag tag)
    {
        _context.Tags.Update(tag);

        await _context.SaveChangesAsync();
    }

    // Delete
    public async Task DeleteAsync(Tag tag)
    {
        _context.Tags.Remove(tag);

        await _context.SaveChangesAsync();
    }

    // Create ke time duplicate slug check
    public async Task<bool> SlugExistsAsync(string slug)
    {
        return await _context.Tags
            .AnyAsync(x => x.Slug == slug);
    }

    // Update ke time current tag ko exclude karke check
    public async Task<bool> SlugExistsAsync(
        string slug,
        int excludeTagId)
    {
        return await _context.Tags
            .AnyAsync(x =>
                x.Slug == slug &&
                x.Id != excludeTagId);
    }
}