using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories
{
    public class PublicSearchRepository : IPublicSearchRepository
    {
        private readonly ApplicationDbContext _context;

        public PublicSearchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<News> News, int TotalCount)> SearchNewsAsync(
            string keyword,
            int pageNumber,
            int pageSize)
        {
            var query = _context.News
                .AsNoTracking()
                .Include(x => x.Category)
                .Where(x =>
                    x.IsPublished &&
                    !x.IsDeleted);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x =>
                    x.Title.Contains(keyword) ||
                    x.ShortDescription.Contains(keyword) ||
                    x.Content.Contains(keyword) ||
                    x.Slug.Contains(keyword));
            }

            var totalCount = await query.CountAsync();

            var news = await query
                .OrderByDescending(x => x.PublishDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (news, totalCount);
        }
    }
}