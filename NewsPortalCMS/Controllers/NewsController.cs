using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.DTOs.News;
using NewsPortalCMS.Models.News;
using NewsPortalCMS.Services.Interfaces;

namespace NewsPortalCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IFileStorageService _fileStorageService;
        private readonly IFileValidationService _fileValidationService;
        private readonly IMediaRepository _mediaRepository;

        public NewsController(
            INewsService newsService,
            IFileStorageService fileStorageService,
            IFileValidationService fileValidationService,
            IMediaRepository mediaRepository)
        {
            _newsService = newsService;
            _fileStorageService = fileStorageService;
            _fileValidationService = fileValidationService;
            _mediaRepository = mediaRepository;
        }

        // ============================================================
        // GET ALL NEWS
        // ============================================================

        [HttpGet]
        
        public async Task<IActionResult> GetAll(
    [FromQuery] NewsQueryRequest request)
        {
            var news = await _newsService.GetAllAsync(request);

            return Ok(news);
        }

        // ============================================================
        // GET NEWS BY ID
        // ============================================================

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var news = await _newsService.GetByIdAsync(id);

            if (news == null)
            {
                return NotFound(new
                {
                    message = "News not found."
                });
            }

            return Ok(news);
        }

        // ============================================================
        // CREATE NEWS
        // ============================================================

        [HttpPost]
        [RequestSizeLimit(1610612736)]
        [RequestFormLimits(MultipartBodyLengthLimit = 1610612736)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(
            [FromForm] CreateNewsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? imagePath = null;
            string? videoPath = null;

            // ========================================================
            // IMAGE UPLOAD
            // ========================================================

            if (request.FeaturedImage != null)
            {
                _fileValidationService.ValidateImage(
                    request.FeaturedImage);

                imagePath =
                    await _fileStorageService.SaveImageWithThumbnailAsync(
                        request.FeaturedImage,
                        "news");
            }

            // ========================================================
            // VIDEO UPLOAD
            // ========================================================

            if (request.FeaturedVideo != null)
            {
                _fileValidationService.ValidateVideo(
                    request.FeaturedVideo);

                videoPath =
                    await _fileStorageService.SaveAsync(
                        request.FeaturedVideo,
                        "videos");
            }

            // ========================================================
            // SAVE IMAGE IN MEDIA LIBRARY
            // ========================================================

            if (request.FeaturedImage != null &&
                !string.IsNullOrEmpty(imagePath))
            {
                var media = new Media
                {
                    FileName = Path.GetFileName(imagePath),

                    OriginalFileName =
                        request.FeaturedImage.FileName,

                    FilePath = imagePath,

                    FileType =
                        Path.GetExtension(
                            request.FeaturedImage.FileName),

                    ContentType =
                        request.FeaturedImage.ContentType,

                    FileSize =
                        request.FeaturedImage.Length,

                    UploadedBy =
                        User.Identity?.Name ?? "Admin",

                    UploadedDate = DateTime.UtcNow,

                    IsActive = true
                };

                await _mediaRepository.AddAsync(media);
            }

            // ========================================================
            // SAVE VIDEO IN MEDIA LIBRARY
            // ========================================================

            if (request.FeaturedVideo != null &&
                !string.IsNullOrEmpty(videoPath))
            {
                var media = new Media
                {
                    FileName = Path.GetFileName(videoPath),

                    OriginalFileName =
                        request.FeaturedVideo.FileName,

                    FilePath = videoPath,

                    FileType =
                        Path.GetExtension(
                            request.FeaturedVideo.FileName),

                    ContentType =
                        request.FeaturedVideo.ContentType,

                    FileSize =
                        request.FeaturedVideo.Length,

                    UploadedBy =
                        User.Identity?.Name ?? "Admin",

                    UploadedDate = DateTime.UtcNow,

                    IsActive = true
                };

                await _mediaRepository.AddAsync(media);
            }

            // ========================================================
            // CREATE NEWS DTO
            // ========================================================

            var dto = new CreateNewsDto
            {
                Title = request.Title,

                Slug = request.Slug,

                ShortDescription =
                    request.ShortDescription,

                Content = request.Content,

                FeaturedImage = imagePath,

                FeaturedVideo = videoPath,

                Author = request.Author,

                PublishDate = request.PublishDate,

                IsPublished = request.IsPublished,

                IsFeatured = request.IsFeatured,

                CategoryId = request.CategoryId
            };

            var createdNews =
                await _newsService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdNews.Id },
                createdNews);
        }

        // ============================================================
        // UPDATE NEWS
        // ============================================================

        [HttpPut("{id:int}")]
        [RequestSizeLimit(1610612736)]
        [RequestFormLimits(MultipartBodyLengthLimit = 1610612736)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(
            int id,
            [FromForm] UpdateNewsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // ========================================================
            // GET EXISTING NEWS
            // ========================================================

            var existingNews =
                await _newsService.GetByIdAsync(id);

            if (existingNews == null)
            {
                return NotFound(new
                {
                    message = "News not found."
                });
            }

            // Keep existing files if no new file is uploaded
            string? imagePath =
                existingNews.FeaturedImage;

            string? videoPath =
                existingNews.FeaturedVideo;

            // ========================================================
            // NEW IMAGE
            // ========================================================

            if (request.FeaturedImage != null)
            {
                _fileValidationService.ValidateImage(
                    request.FeaturedImage);

                var newImagePath =
                    await _fileStorageService.SaveImageWithThumbnailAsync(
                        request.FeaturedImage,
                        "news");

                // Delete old image + thumbnail
                if (!string.IsNullOrWhiteSpace(
                    existingNews.FeaturedImage))
                {
                    await _fileStorageService.DeleteWithThumbnailAsync(
                        existingNews.FeaturedImage);
                }

                imagePath = newImagePath;

                // Save new image in Media Library
                if (!string.IsNullOrEmpty(imagePath))
                {
                    var media = new Media
                    {
                        FileName =
                            Path.GetFileName(imagePath),

                        OriginalFileName =
                            request.FeaturedImage.FileName,

                        FilePath =
                            imagePath,

                        FileType =
                            Path.GetExtension(
                                request.FeaturedImage.FileName),

                        ContentType =
                            request.FeaturedImage.ContentType,

                        FileSize =
                            request.FeaturedImage.Length,

                        UploadedBy =
                            User.Identity?.Name ?? "Admin",

                        UploadedDate =
                            DateTime.UtcNow,

                        IsActive = true
                    };

                    await _mediaRepository.AddAsync(media);
                }
            }

            // ========================================================
            // NEW VIDEO
            // ========================================================

            if (request.FeaturedVideo != null)
            {
                _fileValidationService.ValidateVideo(
                    request.FeaturedVideo);

                var newVideoPath =
                    await _fileStorageService.SaveAsync(
                        request.FeaturedVideo,
                        "videos");

                // Delete OLD VIDEO
                if (!string.IsNullOrWhiteSpace(
                    existingNews.FeaturedVideo))
                {
                    await _fileStorageService.DeleteAsync(
                        existingNews.FeaturedVideo);
                }

                videoPath = newVideoPath;

                // Save new video in Media Library
                if (!string.IsNullOrEmpty(videoPath))
                {
                    var media = new Media
                    {
                        FileName =
                            Path.GetFileName(videoPath),

                        OriginalFileName =
                            request.FeaturedVideo.FileName,

                        FilePath =
                            videoPath,

                        FileType =
                            Path.GetExtension(
                                request.FeaturedVideo.FileName),

                        ContentType =
                            request.FeaturedVideo.ContentType,

                        FileSize =
                            request.FeaturedVideo.Length,

                        UploadedBy =
                            User.Identity?.Name ?? "Admin",

                        UploadedDate =
                            DateTime.UtcNow,

                        IsActive = true
                    };

                    await _mediaRepository.AddAsync(media);
                }
            }

            // ========================================================
            // UPDATE NEWS DTO
            // ========================================================

            var dto = new UpdateNewsDto
            {
                Id = id,

                Title = request.Title,

                Slug = request.Slug,

                ShortDescription =
                    request.ShortDescription,

                Content =
                    request.Content,

                FeaturedImage =
                    imagePath,

                FeaturedVideo =
                    videoPath,

                Author =
                    request.Author,

                PublishDate =
                    request.PublishDate,

                IsPublished =
                    request.IsPublished,

                IsFeatured =
                    request.IsFeatured,

                CategoryId =
                    request.CategoryId
            };

            var updatedNews =
                await _newsService.UpdateAsync(dto);

            if (updatedNews == null)
            {
                return NotFound(new
                {
                    message = "News not found."
                });
            }

            return Ok(updatedNews);
        }

        // ============================================================
        // DELETE NEWS
        // ============================================================

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted =
                await _newsService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "News not found."
                });
            }

            return Ok(new
            {
                message = "News deleted successfully."
            });
        }
    }
}