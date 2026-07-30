using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly ApplicationDbContext _context;

    public MenuItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MenuItem> AddAsync(MenuItem menuItem)
    {
        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync();
        return menuItem;
    }

    public async Task<IEnumerable<MenuItem>> GetAllByMenuIdAsync(int menuId)
    {
        return await _context.MenuItems
            .Where(x => x.MenuId == menuId)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();
    }

    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        return await _context.MenuItems
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<MenuItem> UpdateAsync(MenuItem menuItem)
    {
        _context.MenuItems.Update(menuItem);
        await _context.SaveChangesAsync();
        return menuItem;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.MenuItems.FindAsync(id);

        if (item == null)
            return false;

        _context.MenuItems.Remove(item);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.MenuItems.AnyAsync(x => x.Id == id);
    }
}