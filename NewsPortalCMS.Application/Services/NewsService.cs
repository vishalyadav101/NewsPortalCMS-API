using NewsPortalCMS.DTOs.News;
using NewsPortalCMS.Entities;
using NewsPortalCMS.Interfaces;
using NewsPortalCMS.Services.Interfaces;

namespace NewsPortalCMS.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IEnumerable<NewsDto>> GetAllAsync()
        {
            var newsList = await _newsRepository.GetAllAsync();

            return newsList.Select(n => new NewsDto
            {
                Id = n.Id,
                Title = n.Title,
                Slug = n.Slug,
                ShortDescription = n.ShortDescription,
                Content = n.Content,
                FeaturedImage = n.FeaturedImage,
                Author = n.Author,
                PublishDate = n.PublishDate,
                IsPublished = n.IsPublished,
                ViewCount = n.ViewCount,
                CategoryId = n.CategoryId,
                CategoryName = n.Category?.Name ?? string.Empty,
                CreatedAt = n.CreatedAt,
                UpdatedAt = n.UpdatedAt
            });
        }

        public async Task<NewsDto?> GetByIdAsync(int id)
        {
            var news = await _newsRepository.GetByIdAsync(id);

            if (news == null)
                return null;

            return new NewsDto
            {
                Id = news.Id,
                Title = news.Title,
                Slug = news.Slug,
                ShortDescription = news.ShortDescription,
                Content = news.Content,
                FeaturedImage = news.FeaturedImage,
                Author = news.Author,
                PublishDate = news.PublishDate,
                IsPublished = news.IsPublished,
                ViewCount = news.ViewCount,
                CategoryId = news.CategoryId,
                CategoryName = news.Category?.Name ?? string.Empty,
                CreatedAt = news.CreatedAt,
                UpdatedAt = news.UpdatedAt
            };
        }

        public async Task<NewsDto> CreateAsync(CreateNewsDto dto)
        {
            var news = new News
            {
                Title = dto.Title,
                Slug = dto.Slug,
                ShortDescription = dto.ShortDescription,
                Content = dto.Content,
                FeaturedImage = dto.FeaturedImage,
                Author = dto.Author,
                PublishDate = dto.PublishDate,
                IsPublished = dto.IsPublished,
                CategoryId = dto.CategoryId,
                ViewCount = 0,
                CreatedAt = DateTime.UtcNow
            };

            var createdNews = await _newsRepository.CreateAsync(news);

            return new NewsDto
            {
                Id = createdNews.Id,
                Title = createdNews.Title,
                Slug = createdNews.Slug,
                ShortDescription = createdNews.ShortDescription,
                Content = createdNews.Content,
                FeaturedImage = createdNews.FeaturedImage,
                Author = createdNews.Author,
                PublishDate = createdNews.PublishDate,
                IsPublished = createdNews.IsPublished,
                ViewCount = createdNews.ViewCount,
                CategoryId = createdNews.CategoryId,
                CreatedAt = createdNews.CreatedAt
            };
        }

        public async Task<NewsDto?> UpdateAsync(UpdateNewsDto dto)
        {
            var news = new News
            {
                Id = dto.Id,
                Title = dto.Title,
                Slug = dto.Slug,
                ShortDescription = dto.ShortDescription,
                Content = dto.Content,
                FeaturedImage = dto.FeaturedImage,
                Author = dto.Author,
                PublishDate = dto.PublishDate,
                IsPublished = dto.IsPublished,
                CategoryId = dto.CategoryId
            };

            var updatedNews = await _newsRepository.UpdateAsync(news);

            if (updatedNews == null)
                return null;

            return new NewsDto
            {
                Id = updatedNews.Id,
                Title = updatedNews.Title,
                Slug = updatedNews.Slug,
                ShortDescription = updatedNews.ShortDescription,
                Content = updatedNews.Content,
                FeaturedImage = updatedNews.FeaturedImage,
                Author = updatedNews.Author,
                PublishDate = updatedNews.PublishDate,
                IsPublished = updatedNews.IsPublished,
                ViewCount = updatedNews.ViewCount,
                CategoryId = updatedNews.CategoryId,
                CategoryName = updatedNews.Category?.Name ?? string.Empty,
                CreatedAt = updatedNews.CreatedAt,
                UpdatedAt = updatedNews.UpdatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _newsRepository.DeleteAsync(id);
        }
    }
}