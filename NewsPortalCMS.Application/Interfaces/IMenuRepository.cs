using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories;

public interface IMenuRepository
{
    Task<Menu> AddAsync(Menu menu);

    Task<Menu?> GetByIdAsync(int id);

    Task<IEnumerable<Menu>> GetAllAsync();

    Task<Menu> UpdateAsync(Menu menu);

    Task<bool> DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);

    Task<bool> NameExistsAsync(string name);
    Task<List<Menu>> GetActiveMenusAsync();
    Task<Menu?> GetMenuByLocationAsync(string location);
}