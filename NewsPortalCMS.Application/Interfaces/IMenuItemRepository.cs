using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories;

public interface IMenuItemRepository
{
    Task<MenuItem> AddAsync(MenuItem menuItem);

    Task<MenuItem?> GetByIdAsync(int id);

    Task<IEnumerable<MenuItem>> GetAllByMenuIdAsync(int menuId);

    Task<MenuItem> UpdateAsync(MenuItem menuItem);

    Task<bool> DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);
}