using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories;

public interface IStaticPageRepository
{
    Task<IEnumerable<StaticPage>> GetAllAsync();

    Task<StaticPage?> GetByIdAsync(int id);

    Task<StaticPage?> GetBySlugAsync(string slug);

    Task AddAsync(StaticPage staticPage);

    Task UpdateAsync(StaticPage staticPage);

    Task DeleteAsync(StaticPage staticPage);

    Task<bool> ExistsAsync(int id);
}