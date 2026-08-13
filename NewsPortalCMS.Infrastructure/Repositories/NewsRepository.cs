using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.DTOs.News;
using NewsPortalCMS.Entities;
using NewsPortalCMS.Infrastructure.Data;
using NewsPortalCMS.Interfaces;


namespace NewsPortalCMS.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _context;

        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<News> Items, int TotalCount)> GetAllAsync(
      NewsQueryRequest request)
        {
            var query = _context.News
                .AsNoTracking()
                .Where(n => !n.IsDeleted);

            // Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                query = query.Where(n =>
                    n.Title.Contains(search) ||
                    n.Slug.Contains(search) ||
                    n.ShortDescription.Contains(search));
            }

            // Category filter
            if (request.CategoryId.HasValue)
            {
                query = query.Where(n =>
                    n.CategoryId == request.CategoryId.Value);
            }

            // Published filter
            if (request.IsPublished.HasValue)
            {
                query = query.Where(n =>
                    n.IsPublished == request.IsPublished.Value);
            }

            // Featured filter
            if (request.IsFeatured.HasValue)
            {
                query = query.Where(n =>
                    n.IsFeatured == request.IsFeatured.Value);
            }

            // Count after applying filters
            var totalCount = await query.CountAsync();

            // Sorting
            query = request.SortBy?.ToLower() switch
            {
                "oldest" =>
                    query.OrderBy(n => n.PublishDate),

                "popular" =>
                    query.OrderByDescending(n => n.ViewCount),

                "title" =>
                    query.OrderBy(n => n.Title),

                _ =>
                    query.OrderByDescending(n => n.PublishDate)
            };

            // Pagination
            var items = await query
                .Include(n => n.Category)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return (items, totalCount);
        }
        public async Task<News?> GetByIdAsync(int id)
        {
            return await _context.News
                .Include(n => n.Category)
                .FirstOrDefaultAsync(
                    n => n.Id == id && !n.IsDeleted);
        }

        public async Task<News> CreateAsync(News news)
        {
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();

            return news;
        }

        public async Task<News?> UpdateAsync(News news)
        {
            var existingNews = await _context.News
                .FirstOrDefaultAsync(
                    n => n.Id == news.Id && !n.IsDeleted);

            if (existingNews == null)
                return null;

            existingNews.Title = news.Title;
            existingNews.Slug = news.Slug;
            existingNews.ShortDescription = news.ShortDescription;
            existingNews.Content = news.Content;

            existingNews.FeaturedImage =
                news.FeaturedImage;

            existingNews.FeaturedVideo =
                news.FeaturedVideo;

            existingNews.Author = news.Author;
            existingNews.PublishDate = news.PublishDate;
            existingNews.IsPublished = news.IsPublished;
            existingNews.IsFeatured = news.IsFeatured;
            existingNews.CategoryId = news.CategoryId;

            existingNews.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Reload category so CategoryName is available
            await _context.Entry(existingNews)
                .Reference(n => n.Category)
                .LoadAsync();

            return existingNews;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var news = await _context.News
                .FirstOrDefaultAsync(
                    n => n.Id == id && !n.IsDeleted);

            if (news == null)
                return false;

            news.IsDeleted = true;
            news.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RestoreAsync(int id)
        {
            var news = await _context.News
                .FirstOrDefaultAsync(n => n.Id == id);

            if (news == null)
                return false;

            news.IsDeleted = false;
            news.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.News
                .AnyAsync(
                    n => n.Id == id && !n.IsDeleted);
        }

        public async Task<bool> SlugExistsAsync(string slug)
        {
            return await _context.News
                .AnyAsync(
                    n => n.Slug.ToLower() == slug.ToLower()
                         && !n.IsDeleted);
        }

        public async Task<bool> TitleExistsAsync(string title)
        {
            return await _context.News
                .AnyAsync(
                    n => n.Title.ToLower() == title.ToLower()
                         && !n.IsDeleted);
        }

        public async Task<bool> CategoryExistsAsync(
            int categoryId)
        {
            return await _context.Categories
                .AnyAsync(c => c.Id == categoryId);
        }
    }
}