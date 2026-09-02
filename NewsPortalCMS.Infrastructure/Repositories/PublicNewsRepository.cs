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

        // ============================================================
        // LATEST NEWS
        // ============================================================

        public async Task<IEnumerable<News>> GetLatestNewsAsync(int count)
        {
            count = NormalizeCount(count);

            return await _context.News
                .AsNoTracking()
                .Where(n =>
                    n.IsPublished &&
                    !n.IsDeleted)
                .Include(n => n.Category)
                .OrderByDescending(n => n.PublishDate)
                .Take(count)
                .ToListAsync();
        }

        // ============================================================
        // FEATURED NEWS
        // ============================================================

        public async Task<IEnumerable<News>> GetFeaturedNewsAsync(int count)
        {
            count = NormalizeCount(count);

            return await _context.News
                .AsNoTracking()
                .Where(n =>
                    n.IsPublished &&
                    !n.IsDeleted &&
                    n.IsFeatured)
                .Include(n => n.Category)
                .OrderByDescending(n => n.PublishDate)
                .Take(count)
                .ToListAsync();
        }

        // ============================================================
        // POPULAR NEWS
        // ============================================================

        public async Task<IEnumerable<News>> GetPopularNewsAsync(int count)
        {
            count = NormalizeCount(count);

            return await _context.News
                .AsNoTracking()
                .Where(n =>
                    n.IsPublished &&
                    !n.IsDeleted)
                .Include(n => n.Category)
                .OrderByDescending(n => n.ViewCount)
                .ThenByDescending(n => n.PublishDate)
                .Take(count)
                .ToListAsync();
        }

        // ============================================================
        // NEWS BY CATEGORY
        // ============================================================

        public async Task<IEnumerable<News>> GetNewsByCategoryAsync(
            int categoryId)
        {
            if (categoryId <= 0)
            {
                return Enumerable.Empty<News>();
            }

            return await _context.News
                .AsNoTracking()
                .Where(n =>
                    n.CategoryId == categoryId &&
                    n.IsPublished &&
                    !n.IsDeleted)
                .Include(n => n.Category)
                .OrderByDescending(n => n.PublishDate)
                .ToListAsync();
        }
        // ============================================================
        // NEWS BY SUBCATEGORY
        // ============================================================

        public async Task<IEnumerable<News>> GetNewsBySubcategoryAsync(
            int subcategoryId)
        {
            if (subcategoryId <= 0)
            {
                return Enumerable.Empty<News>();
            }

            return await _context.News
                .AsNoTracking()
                .Where(n =>
                    n.SubCategoryId == subcategoryId &&
                    n.IsPublished &&
                    !n.IsDeleted)
                .Include(n => n.Category)
                .OrderByDescending(n => n.PublishDate)
                .ToListAsync();
        }

        // ============================================================
        // SEARCH NEWS
        // ============================================================

        public async Task<IEnumerable<News>> SearchNewsAsync(
            string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return Enumerable.Empty<News>();
            }

            keyword = keyword.Trim();

            return await _context.News
                .AsNoTracking()
                .Where(n =>
                    n.IsPublished &&
                    !n.IsDeleted &&
                    (
                        n.Title.Contains(keyword) ||
                        n.ShortDescription.Contains(keyword)
                    ))
                .Include(n => n.Category)
                .OrderByDescending(n => n.PublishDate)
                .ToListAsync();
        }

        // ============================================================
        // NEWS DETAILS BY SLUG
        // ============================================================

        public async Task<News?> GetNewsBySlugAsync(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return null;
            }

            slug = slug.Trim();

            return await _context.News
                .AsNoTracking()
                .Where(n =>
                    n.Slug == slug &&
                    n.IsPublished &&
                    !n.IsDeleted)
                .Include(n => n.Category)
                .Include(n => n.NewsTags)
                    .ThenInclude(nt => nt.Tag)
                .Include(n => n.Comments)
                .FirstOrDefaultAsync();
        }

        // ============================================================
        // COUNT NORMALIZATION
        // ============================================================

        private static int NormalizeCount(int count)
        {
            if (count <= 0)
            {
                return 10;
            }

            return Math.Min(count, 50);
        }
    }
}