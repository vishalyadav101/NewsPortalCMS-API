using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;
using NewsPortalCMS.Application.Interfaces.Repositories;

namespace NewsPortalCMS.Infrastructure.Repositories;

public class WebsiteSettingRepository : IWebsiteSettingRepository
{
    private readonly ApplicationDbContext _context;

    public WebsiteSettingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WebsiteSetting?> GetAsync()
    {
        return await _context.WebsiteSettings
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<WebsiteSetting?> GetByIdAsync(int id)
    {
        return await _context.WebsiteSettings
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<WebsiteSetting> AddAsync(
        WebsiteSetting entity)
    {
        await _context.WebsiteSettings.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(
        WebsiteSetting entity)
    {
        _context.WebsiteSettings.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(
        WebsiteSetting entity)
    {
        _context.WebsiteSettings.Remove(entity);
        await _context.SaveChangesAsync();
    }
}