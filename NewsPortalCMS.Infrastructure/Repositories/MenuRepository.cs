using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly ApplicationDbContext _context;

    public MenuRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Menu> AddAsync(Menu menu)
    {
        _context.Menus.Add(menu);
        await _context.SaveChangesAsync();
        return menu;
    }

    public async Task<IEnumerable<Menu>> GetAllAsync()
    {
        return await _context.Menus
            .OrderBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<Menu?> GetByIdAsync(int id)
    {
        return await _context.Menus
            .Include(m => m.MenuItems)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Menu> UpdateAsync(Menu menu)
    {
        _context.Menus.Update(menu);
        await _context.SaveChangesAsync();
        return menu;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var menu = await _context.Menus.FindAsync(id);

        if (menu == null)
            return false;

        _context.Menus.Remove(menu);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Menus.AnyAsync(m => m.Id == id);
    }

    public async Task<bool> NameExistsAsync(string name)
    {
        return await _context.Menus.AnyAsync(m => m.Name == name);
    }
    public async Task<Menu?> GetMenuByLocationAsync(string location)
    {
        return await _context.Menus
            .Include(x => x.MenuItems)
            .FirstOrDefaultAsync(x =>
                x.Location.ToLower() == location.ToLower()
                && x.IsActive);
    }
    public async Task<List<Menu>> GetActiveMenusAsync()
    {
        return await _context.Menus
            .Include(x => x.MenuItems)
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }
}