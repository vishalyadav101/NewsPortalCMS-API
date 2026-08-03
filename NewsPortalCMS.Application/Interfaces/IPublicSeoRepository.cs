using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories
{
    public interface IPublicSeoRepository
    {
        Task<Seo?> GetByPageNameAsync(string pageName);
    }
}