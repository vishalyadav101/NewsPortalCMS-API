using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await _context.News
                .Where(n => !n.IsDeleted)
                .Include(n => n.Category)
                .OrderByDescending(n => n.PublishDate)
                .ToListAsync();
        }

        public async Task<News?> GetByIdAsync(int id)
        {
            return await _context.News
                .Include(n => n.Category)
                .FirstOrDefaultAsync(n => n.Id == id && !n.IsDeleted);
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
                .FirstOrDefaultAsync(n => n.Id == news.Id && !n.IsDeleted);

            if (existingNews == null)
                return null;

            existingNews.Title = news.Title;
            existingNews.Slug = news.Slug;
            existingNews.ShortDescription = news.ShortDescription;
            existingNews.Content = news.Content;
            existingNews.FeaturedImage = news.FeaturedImage;
            existingNews.Author = news.Author;
            existingNews.PublishDate = news.PublishDate;
            existingNews.IsPublished = news.IsPublished;
            existingNews.CategoryId = news.CategoryId;
            existingNews.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingNews;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var news = await _context.News
                .FirstOrDefaultAsync(n => n.Id == id && !n.IsDeleted);

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
                .AnyAsync(n => n.Id == id && !n.IsDeleted);
        }

        public async Task<bool> SlugExistsAsync(string slug)
        {
            return await _context.News
                .AnyAsync(n => n.Slug.ToLower() == slug.ToLower() && !n.IsDeleted);
        }

        public async Task<bool> TitleExistsAsync(string title)
        {
            return await _context.News
                .AnyAsync(n => n.Title.ToLower() == title.ToLower() && !n.IsDeleted);
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _context.Categories
                .AnyAsync(c => c.Id == categoryId);
        }
    }
}