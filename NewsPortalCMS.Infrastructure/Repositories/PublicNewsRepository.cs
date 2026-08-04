using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories
{
    public class PublicNewsRepository : IPublicNewsRepository
    {
        private readonly ApplicationDbContext _context;

        public PublicNewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetLatestNewsAsync(int count)
        {
            return await _context.News
                .AsNoTracking()
                .Include(n => n.Category)
                .Where(n => n.IsPublished && !n.IsDeleted)
                .OrderByDescending(n => n.PublishDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> GetFeaturedNewsAsync(int count)
        {
            return await _context.News
                .AsNoTracking()
                .Include(n => n.Category)
                .Where(n =>
                    n.IsPublished &&
                    !n.IsDeleted &&
                    n.IsFeatured)
                .OrderByDescending(n => n.PublishDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<News?> GetNewsBySlugAsync(string slug)
        {
            return await _context.News
                .AsNoTracking()
                .Include(n => n.Category)
                .Include(n => n.NewsTags)
                    .ThenInclude(nt => nt.Tag)
                .Include(n => n.Comments)
                .FirstOrDefaultAsync(n =>
                    n.Slug == slug &&
                    n.IsPublished &&
                    !n.IsDeleted);
        }

        public async Task<IEnumerable<News>> GetNewsByCategoryAsync(int categoryId)
        {
            return await _context.News
                .AsNoTracking()
                .Include(n => n.Category)
                .Where(n =>
                    n.CategoryId == categoryId &&
                    n.IsPublished &&
                    !n.IsDeleted)
                .OrderByDescending(n => n.PublishDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> SearchNewsAsync(string keyword)
        {
            keyword = keyword.Trim();

            return await _context.News
                .AsNoTracking()
                .Include(n => n.Category)
                .Where(n =>
                    n.IsPublished &&
                    !n.IsDeleted &&
                    (n.Title.Contains(keyword) ||
                     n.ShortDescription.Contains(keyword)))
                .OrderByDescending(n => n.PublishDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<News>> GetPopularNewsAsync(int count)
        {
            return await _context.News
                .AsNoTracking()
                .Include(n => n.Category)
                .Where(n =>
                    n.IsPublished &&
                    !n.IsDeleted)
                .OrderByDescending(n => n.ViewCount)
                .Take(count)
                .ToListAsync();
        }
    }
}