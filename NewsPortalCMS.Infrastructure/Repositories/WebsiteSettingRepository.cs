using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories;

public class WebsiteSettingRepository : IWebsiteSettingRepository
{
    private readonly ApplicationDbContext _context;

    public WebsiteSettingRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<WebsiteSetting?> GetAsync()
    {
        return await _context.WebsiteSettings
            .FirstOrDefaultAsync();
    }


    public async Task<WebsiteSetting?> GetByIdAsync(int id)
    {
        return await _context.WebsiteSettings
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task<WebsiteSetting> AddAsync(
        WebsiteSetting websiteSetting)
    {
        await _context.WebsiteSettings.AddAsync(
            websiteSetting);

        await _context.SaveChangesAsync();

        return websiteSetting;
    }


    public async Task UpdateAsync(
        WebsiteSetting websiteSetting)
    {
        _context.WebsiteSettings.Update(
            websiteSetting);

        await _context.SaveChangesAsync();
    }


    public async Task<bool> DeleteAsync(int id)
    {
        var setting =
            await GetByIdAsync(id);


        if (setting == null)
        {
            return false;
        }


        _context.WebsiteSettings.Remove(setting);

        await _context.SaveChangesAsync();

        return true;
    }
}