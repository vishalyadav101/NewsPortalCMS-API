using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories;

public class StaticPageRepository : IStaticPageRepository
{
    private readonly ApplicationDbContext _context;


    public StaticPageRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<StaticPage>> GetAllAsync()
    {
        return await _context.StaticPages
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync();
    }


    public async Task<StaticPage?> GetByIdAsync(int id)
    {
        return await _context.StaticPages
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<StaticPage?> GetBySlugAsync(string slug)
    {
        return await _context.StaticPages
            .FirstOrDefaultAsync(x => x.Slug == slug);
    }


    public async Task AddAsync(StaticPage staticPage)
    {
        await _context.StaticPages.AddAsync(staticPage);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateAsync(StaticPage staticPage)
    {
        _context.StaticPages.Update(staticPage);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(StaticPage staticPage)
    {
        _context.StaticPages.Remove(staticPage);
        await _context.SaveChangesAsync();
    }


    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.StaticPages
            .AnyAsync(x => x.Id == id);
    }
}