using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories
{
    public class PublicSeoRepository : IPublicSeoRepository
    {
        private readonly ApplicationDbContext _context;

        public PublicSeoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Seo?> GetByPageNameAsync(string pageName)
        {
            return await _context.Seos
                .FirstOrDefaultAsync(x =>
                    x.PageName == pageName &&
                    x.IsActive);
        }
    }
}