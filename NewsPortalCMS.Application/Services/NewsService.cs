using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.DTOs.News;
using NewsPortalCMS.Entities;
using NewsPortalCMS.Interfaces;
using NewsPortalCMS.Services.Interfaces;

namespace NewsPortalCMS.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IFileStorageService _fileStorageService;

        public NewsService(
            INewsRepository newsRepository,
            IFileStorageService fileStorageService)
        {
            _newsRepository = newsRepository;
            _fileStorageService = fileStorageService;
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
                FeaturedVideo = n.FeaturedVideo,
                Author = n.Author,
                PublishDate = n.PublishDate,
                IsPublished = n.IsPublished,
                IsFeatured = n.IsFeatured,
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
                FeaturedVideo = news.FeaturedVideo,
                Author = news.Author,
                PublishDate = news.PublishDate,
                IsPublished = news.IsPublished,
                IsFeatured = news.IsFeatured,
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
                FeaturedVideo = dto.FeaturedVideo,
                Author = dto.Author,
                PublishDate = dto.PublishDate,
                IsPublished = dto.IsPublished,
                IsFeatured = dto.IsFeatured,
                CategoryId = dto.CategoryId,
                ViewCount = 0,
                CreatedAt = DateTime.UtcNow
            };

            var createdNews =
                await _newsRepository.CreateAsync(news);

            return new NewsDto
            {
                Id = createdNews.Id,
                Title = createdNews.Title,
                Slug = createdNews.Slug,
                ShortDescription = createdNews.ShortDescription,
                Content = createdNews.Content,
                FeaturedImage = createdNews.FeaturedImage,
                FeaturedVideo = createdNews.FeaturedVideo,
                Author = createdNews.Author,
                PublishDate = createdNews.PublishDate,
                IsPublished = createdNews.IsPublished,
                IsFeatured = createdNews.IsFeatured,
                ViewCount = createdNews.ViewCount,
                CategoryId = createdNews.CategoryId,
                CategoryName =
                    createdNews.Category?.Name ?? string.Empty,
                CreatedAt = createdNews.CreatedAt,
                UpdatedAt = createdNews.UpdatedAt
            };
        }

        public async Task<NewsDto?> UpdateAsync(UpdateNewsDto dto)
        {
            var existingNews =
                await _newsRepository.GetByIdAsync(dto.Id);

            if (existingNews == null)
                return null;

            // Keep existing image/video if no new file was uploaded
            var imagePath =
                string.IsNullOrWhiteSpace(dto.FeaturedImage)
                    ? existingNews.FeaturedImage
                    : dto.FeaturedImage;

            var videoPath =
                string.IsNullOrWhiteSpace(dto.FeaturedVideo)
                    ? existingNews.FeaturedVideo
                    : dto.FeaturedVideo;

            // Delete old image if a new image was uploaded
            if (!string.IsNullOrWhiteSpace(dto.FeaturedImage) &&
                !string.IsNullOrWhiteSpace(existingNews.FeaturedImage) &&
                existingNews.FeaturedImage != dto.FeaturedImage)
            {
                await _fileStorageService.DeleteWithThumbnailAsync(
                    existingNews.FeaturedImage);
            }

            // Delete old video if a new video was uploaded
            if (!string.IsNullOrWhiteSpace(dto.FeaturedVideo) &&
                !string.IsNullOrWhiteSpace(existingNews.FeaturedVideo) &&
                existingNews.FeaturedVideo != dto.FeaturedVideo)
            {
                await _fileStorageService.DeleteAsync(
                    existingNews.FeaturedVideo);
            }

            var news = new News
            {
                Id = dto.Id,
                Title = dto.Title,
                Slug = dto.Slug,
                ShortDescription = dto.ShortDescription,
                Content = dto.Content,
                FeaturedImage = imagePath,
                FeaturedVideo = videoPath,
                Author = dto.Author,
                PublishDate = dto.PublishDate,
                IsPublished = dto.IsPublished,
                IsFeatured = dto.IsFeatured,
                CategoryId = dto.CategoryId
            };

            var updatedNews =
                await _newsRepository.UpdateAsync(news);

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
                FeaturedVideo = updatedNews.FeaturedVideo,
                Author = updatedNews.Author,
                PublishDate = updatedNews.PublishDate,
                IsPublished = updatedNews.IsPublished,
                IsFeatured = updatedNews.IsFeatured,
                ViewCount = updatedNews.ViewCount,
                CategoryId = updatedNews.CategoryId,
                CategoryName =
                    updatedNews.Category?.Name ?? string.Empty,
                CreatedAt = updatedNews.CreatedAt,
                UpdatedAt = updatedNews.UpdatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var news =
                await _newsRepository.GetByIdAsync(id);

            if (news == null)
                return false;

            var deleted =
                await _newsRepository.DeleteAsync(id);

            if (!deleted)
                return false;

            // Delete featured image + thumbnail
            if (!string.IsNullOrWhiteSpace(news.FeaturedImage))
            {
                await _fileStorageService
                    .DeleteWithThumbnailAsync(
                        news.FeaturedImage);
            }

            // Delete featured video
            if (!string.IsNullOrWhiteSpace(news.FeaturedVideo))
            {
                await _fileStorageService
                    .DeleteAsync(
                        news.FeaturedVideo);
            }

            return true;
        }
    }
}