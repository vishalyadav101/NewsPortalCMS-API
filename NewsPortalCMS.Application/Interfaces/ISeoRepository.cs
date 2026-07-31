using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories
{
    public interface ISeoRepository
    {
        Task<IEnumerable<Seo>> GetAllAsync();

        Task<Seo?> GetByIdAsync(int id);

        Task<Seo?> GetByPageNameAsync(string pageName);

        Task<Seo> CreateAsync(Seo seo);

        Task<Seo?> UpdateAsync(Seo seo);

        Task<bool> DeleteAsync(int id);
    }
}