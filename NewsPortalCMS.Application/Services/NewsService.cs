using NewsPortalCMS.Application.Common.Pagination;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.DTOs.News;
using NewsPortalCMS.Entities;
using NewsPortalCMS.Interfaces;
using NewsPortalCMS.Services.Interfaces;

namespace NewsPortalCMS.Services
{
    public class NewsService : INewsService
    {
        private readonly ICacheService _cacheService;
        private readonly INewsRepository _newsRepository;
        private readonly IFileStorageService _fileStorageService;

        public NewsService(
            INewsRepository newsRepository,
            IFileStorageService fileStorageService,
            ICacheService cacheService)
        {
            _newsRepository = newsRepository;
            _fileStorageService = fileStorageService;
            _cacheService = cacheService;
        }

        // ============================================================
        // CLEAR PUBLIC NEWS CACHE
        // ============================================================

        private void ClearPublicNewsCache(
            int? oldCategoryId = null,
            int? newCategoryId = null)
        {
            _cacheService.Remove(
                "public_news_latest_10");

            _cacheService.Remove(
                "public_news_featured_10");

            _cacheService.Remove(
                "public_news_popular_10");

            if (oldCategoryId.HasValue)
            {
                _cacheService.Remove(
                    $"public_news_category_{oldCategoryId.Value}");
            }

            if (newCategoryId.HasValue)
            {
                _cacheService.Remove(
                    $"public_news_category_{newCategoryId.Value}");
            }
        }

        // ============================================================
        // GET ALL NEWS
        // ============================================================

        public async Task<PaginatedResponse<NewsDto>> GetAllAsync(
            NewsQueryRequest request)
        {
            var result =
                await _newsRepository.GetAllAsync(request);

            var newsDtos =
                result.Items.Select(n => new NewsDto
                {
                    Id = n.Id,

                    Title = n.Title,

                    Slug = n.Slug,

                    ShortDescription =
                        n.ShortDescription,

                    Content =
                        n.Content,

                    FeaturedImage =
                        n.FeaturedImage,

                    FeaturedVideo =
                        n.FeaturedVideo,

                    Author =
                        n.Author ?? string.Empty,

                    PublishDate =
                        n.PublishDate,

                    IsPublished =
                        n.IsPublished,

                    IsFeatured =
                        n.IsFeatured,

                    ViewCount =
                        n.ViewCount,

                    // ====================================================
                    // CATEGORY
                    // ====================================================

                    CategoryId =
                        n.CategoryId,

                    CategoryName =
                        n.Category?.Name ??
                        string.Empty,

                    // ====================================================
                    // SUB CATEGORY
                    // ====================================================

                    SubCategoryId =
                        n.SubCategoryId,

                    SubCategoryName =
                        n.SubCategory?.Name ??
                        string.Empty,

                    // ====================================================
                    // AUDIT
                    // ====================================================

                    CreatedAt =
                        n.CreatedAt,

                    UpdatedAt =
                        n.UpdatedAt

                }).ToList();

            // ============================================================
            // PAGINATION
            // ============================================================

            if (request.PageNumber.HasValue &&
                request.PageSize.HasValue)
            {
                var pageNumber =
                    request.PageNumber.Value;

                var pageSize =
                    request.PageSize.Value;

                var totalPages =
                    (int)Math.Ceiling(
                        result.TotalCount /
                        (double)pageSize);

                return new PaginatedResponse<NewsDto>
                {
                    Items =
                        newsDtos,

                    PageNumber =
                        pageNumber,

                    PageSize =
                        pageSize,

                    TotalCount =
                        result.TotalCount,

                    TotalPages =
                        totalPages,

                    HasPreviousPage =
                        pageNumber > 1,

                    HasNextPage =
                        pageNumber < totalPages
                };
            }

            // ============================================================
            // NO PAGINATION
            // ============================================================

            return new PaginatedResponse<NewsDto>
            {
                Items =
                    newsDtos,

                PageNumber = 1,

                PageSize =
                    newsDtos.Count,

                TotalCount =
                    result.TotalCount,

                TotalPages =
                    newsDtos.Count > 0
                        ? 1
                        : 0,

                HasPreviousPage =
                    false,

                HasNextPage =
                    false
            };
        }

        // ============================================================
        // GET NEWS BY ID
        // ============================================================

        public async Task<NewsDto?> GetByIdAsync(int id)
        {
            var news =
                await _newsRepository.GetByIdAsync(id);

            if (news == null)
            {
                return null;
            }

            return new NewsDto
            {
                Id =
                    news.Id,

                Title =
                    news.Title,

                Slug =
                    news.Slug,

                ShortDescription =
                    news.ShortDescription,

                Content =
                    news.Content,

                FeaturedImage =
                    news.FeaturedImage,

                FeaturedVideo =
                    news.FeaturedVideo,

                Author =
                    news.Author ??
                    string.Empty,

                PublishDate =
                    news.PublishDate,

                IsPublished =
                    news.IsPublished,

                IsFeatured =
                    news.IsFeatured,

                ViewCount =
                    news.ViewCount,

                // ========================================================
                // CATEGORY
                // ========================================================

                CategoryId =
                    news.CategoryId,

                CategoryName =
                    news.Category?.Name ??
                    string.Empty,

                // ========================================================
                // SUB CATEGORY
                // ========================================================

                SubCategoryId =
                    news.SubCategoryId,

                SubCategoryName =
                    news.SubCategory?.Name ??
                    string.Empty,

                // ========================================================
                // AUDIT
                // ========================================================

                CreatedAt =
                    news.CreatedAt,

                UpdatedAt =
                    news.UpdatedAt
            };
        }

        // ============================================================
        // CREATE NEWS
        // ============================================================

        public async Task<NewsDto> CreateAsync(
            CreateNewsDto dto)
        {
            var news = new News
            {
                Title =
                    dto.Title,

                Slug =
                    dto.Slug,

                ShortDescription =
                    dto.ShortDescription,

                Content =
                    dto.Content,

                FeaturedImage =
                    dto.FeaturedImage,

                FeaturedVideo =
                    dto.FeaturedVideo,

                Author =
                    dto.Author,

                PublishDate =
                    dto.PublishDate,

                IsPublished =
                    dto.IsPublished,

                IsFeatured =
                    dto.IsFeatured,

                // ====================================================
                // CATEGORY
                // ====================================================

                CategoryId =
                    dto.CategoryId,

                // ====================================================
                // SUB CATEGORY
                // ====================================================

                SubCategoryId =
                    dto.SubCategoryId,

                ViewCount = 0,

                CreatedAt =
                    DateTime.UtcNow
            };

            var createdNews =
                await _newsRepository.CreateAsync(
                    news);

            ClearPublicNewsCache(
                createdNews.CategoryId);

            return new NewsDto
            {
                Id =
                    createdNews.Id,

                Title =
                    createdNews.Title,

                Slug =
                    createdNews.Slug,

                ShortDescription =
                    createdNews.ShortDescription,

                Content =
                    createdNews.Content,

                FeaturedImage =
                    createdNews.FeaturedImage,

                FeaturedVideo =
                    createdNews.FeaturedVideo,

                Author =
                    createdNews.Author ??
                    string.Empty,

                PublishDate =
                    createdNews.PublishDate,

                IsPublished =
                    createdNews.IsPublished,

                IsFeatured =
                    createdNews.IsFeatured,

                ViewCount =
                    createdNews.ViewCount,

                // ====================================================
                // CATEGORY
                // ====================================================

                CategoryId =
                    createdNews.CategoryId,

                CategoryName =
                    createdNews.Category?.Name ??
                    string.Empty,

                // ====================================================
                // SUB CATEGORY
                // ====================================================

                SubCategoryId =
                    createdNews.SubCategoryId,

                SubCategoryName =
                    createdNews.SubCategory?.Name ??
                    string.Empty,

                CreatedAt =
                    createdNews.CreatedAt,

                UpdatedAt =
                    createdNews.UpdatedAt
            };
        }

        // ============================================================
        // UPDATE NEWS
        // ============================================================

        public async Task<NewsDto?> UpdateAsync(
            UpdateNewsDto dto)
        {
            var existingNews =
                await _newsRepository.GetByIdAsync(
                    dto.Id);

            if (existingNews == null)
            {
                return null;
            }

            // ========================================================
            // KEEP EXISTING IMAGE
            // ========================================================

            var imagePath =
                string.IsNullOrWhiteSpace(
                    dto.FeaturedImage)
                    ? existingNews.FeaturedImage
                    : dto.FeaturedImage;

            // ========================================================
            // KEEP EXISTING VIDEO
            // ========================================================

            var videoPath =
                string.IsNullOrWhiteSpace(
                    dto.FeaturedVideo)
                    ? existingNews.FeaturedVideo
                    : dto.FeaturedVideo;

            // ========================================================
            // DELETE OLD IMAGE
            // ========================================================

            if (!string.IsNullOrWhiteSpace(
                    dto.FeaturedImage) &&
                !string.IsNullOrWhiteSpace(
                    existingNews.FeaturedImage) &&
                existingNews.FeaturedImage !=
                    dto.FeaturedImage)
            {
                await _fileStorageService
                    .DeleteWithThumbnailAsync(
                        existingNews.FeaturedImage);
            }

            // ========================================================
            // DELETE OLD VIDEO
            // ========================================================

            if (!string.IsNullOrWhiteSpace(
                    dto.FeaturedVideo) &&
                !string.IsNullOrWhiteSpace(
                    existingNews.FeaturedVideo) &&
                existingNews.FeaturedVideo !=
                    dto.FeaturedVideo)
            {
                await _fileStorageService
                    .DeleteAsync(
                        existingNews.FeaturedVideo);
            }

            // ========================================================
            // CREATE UPDATED ENTITY
            // ========================================================

            var news = new News
            {
                Id =
                    dto.Id,

                Title =
                    dto.Title,

                Slug =
                    dto.Slug,

                ShortDescription =
                    dto.ShortDescription,

                Content =
                    dto.Content,

                FeaturedImage =
                    imagePath,

                FeaturedVideo =
                    videoPath,

                Author =
                    dto.Author,

                PublishDate =
                    dto.PublishDate,

                IsPublished =
                    dto.IsPublished,

                IsFeatured =
                    dto.IsFeatured,

                // ====================================================
                // CATEGORY
                // ====================================================

                CategoryId =
                    dto.CategoryId,

                // ====================================================
                // SUB CATEGORY
                // ====================================================

                SubCategoryId =
                    dto.SubCategoryId
            };

            var updatedNews =
                await _newsRepository.UpdateAsync(
                    news);

            if (updatedNews == null)
            {
                return null;
            }

            ClearPublicNewsCache(
                updatedNews.CategoryId);

            return new NewsDto
            {
                Id =
                    updatedNews.Id,

                Title =
                    updatedNews.Title,

                Slug =
                    updatedNews.Slug,

                ShortDescription =
                    updatedNews.ShortDescription,

                Content =
                    updatedNews.Content,

                FeaturedImage =
                    updatedNews.FeaturedImage,

                FeaturedVideo =
                    updatedNews.FeaturedVideo,

                Author =
                    updatedNews.Author ??
                    string.Empty,

                PublishDate =
                    updatedNews.PublishDate,

                IsPublished =
                    updatedNews.IsPublished,

                IsFeatured =
                    updatedNews.IsFeatured,

                ViewCount =
                    updatedNews.ViewCount,

                // ====================================================
                // CATEGORY
                // ====================================================

                CategoryId =
                    updatedNews.CategoryId,

                CategoryName =
                    updatedNews.Category?.Name ??
                    string.Empty,

                // ====================================================
                // SUB CATEGORY
                // ====================================================

                SubCategoryId =
                    updatedNews.SubCategoryId,

                SubCategoryName =
                    updatedNews.SubCategory?.Name ??
                    string.Empty,

                CreatedAt =
                    updatedNews.CreatedAt,

                UpdatedAt =
                    updatedNews.UpdatedAt
            };
        }

        // ============================================================
        // DELETE NEWS
        // ============================================================

        public async Task<bool> DeleteAsync(int id)
        {
            var news =
                await _newsRepository.GetByIdAsync(id);

            if (news == null)
            {
                return false;
            }

            var deleted =
                await _newsRepository.DeleteAsync(id);

            if (!deleted)
            {
                return false;
            }

            ClearPublicNewsCache(
                news.CategoryId);

            // ========================================================
            // DELETE FEATURED IMAGE
            // ========================================================

            if (!string.IsNullOrWhiteSpace(
                news.FeaturedImage))
            {
                await _fileStorageService
                    .DeleteWithThumbnailAsync(
                        news.FeaturedImage);
            }

            // ========================================================
            // DELETE FEATURED VIDEO
            // ========================================================

            if (!string.IsNullOrWhiteSpace(
                news.FeaturedVideo))
            {
                await _fileStorageService
                    .DeleteAsync(
                        news.FeaturedVideo);
            }

            return true;
        }
    }
}