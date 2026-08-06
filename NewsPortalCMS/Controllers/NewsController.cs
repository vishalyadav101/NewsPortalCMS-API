using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.DTOs.News;
using NewsPortalCMS.Models.News;
using NewsPortalCMS.Services;
using NewsPortalCMS.Services.Interfaces;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly FileService _fileService;
        private readonly IMediaRepository _mediaRepository;

        public NewsController(
            INewsService newsService,
            FileService fileService,
            IMediaRepository mediaRepository)
        {
            _newsService = newsService;
            _fileService = fileService;
            _mediaRepository = mediaRepository;
        }

        // GET: api/News
        
        [HttpGet]
        [RequestSizeLimit(1610612736)]
        [RequestFormLimits(MultipartBodyLengthLimit = 1610612736)]
        public async Task<IActionResult> GetAll()
        {
            var news = await _newsService.GetAllAsync();
            return Ok(news);
        }

        // GET: api/News/5
        
        [HttpGet("{id:int}")]
        [RequestSizeLimit(1610612736)]
        [RequestFormLimits(MultipartBodyLengthLimit = 1610612736)]
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

        // POST: api/News
        [HttpPost]
        [RequestSizeLimit(1610612736)]
        [RequestFormLimits(MultipartBodyLengthLimit = 1610612736)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateNewsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? imagePath =
    await _fileService.UploadNewsImageAsync(request.FeaturedImage);

string? videoPath =
    await _fileService.UploadNewsVideoAsync(request.FeaturedVideo);

            // Save image in Media Library
            if (request.FeaturedImage != null && !string.IsNullOrEmpty(imagePath))
            {
                var media = new Media
                {
                    FileName = Path.GetFileName(imagePath),
                    OriginalFileName = request.FeaturedImage.FileName,
                    FilePath = imagePath,
                    FileType = Path.GetExtension(request.FeaturedImage.FileName),
                    ContentType = request.FeaturedImage.ContentType,
                    FileSize = request.FeaturedImage.Length,
                    UploadedBy = User.Identity?.Name ?? "Admin",
                    UploadedDate = DateTime.UtcNow,
                    IsActive = true
                };

                await _mediaRepository.AddAsync(media);
            }
            // Save video in Media Library
            if (request.FeaturedVideo != null && !string.IsNullOrEmpty(videoPath))
            {
                var media = new Media
                {
                    FileName = Path.GetFileName(videoPath),
                    OriginalFileName = request.FeaturedVideo.FileName,
                    FilePath = videoPath,
                    FileType = Path.GetExtension(request.FeaturedVideo.FileName),
                    ContentType = request.FeaturedVideo.ContentType,
                    FileSize = request.FeaturedVideo.Length,
                    UploadedBy = User.Identity?.Name ?? "Admin",
                    UploadedDate = DateTime.UtcNow,
                    IsActive = true
                };

                await _mediaRepository.AddAsync(media);
            }

            var dto = new CreateNewsDto
            {
                Title = request.Title,
                Slug = request.Slug,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImage = imagePath,
                FeaturedVideo = videoPath,
                Author = request.Author,
                PublishDate = request.PublishDate,
                IsPublished = request.IsPublished,
                IsFeatured = request.IsFeatured,
                CategoryId = request.CategoryId
            };

            var createdNews = await _newsService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdNews.Id }, createdNews);
        }

        // PUT: api/News/5
        // PUT: api/News/5
        [HttpPut("{id:int}")]
        [RequestSizeLimit(1610612736)]
        [RequestFormLimits(MultipartBodyLengthLimit = 1610612736)]
        [Consumes("multipart/form-data")]
        // PUT: api/News/5
       
        public async Task<IActionResult> Update(
    int id,
    [FromForm] UpdateNewsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? imagePath = null;
            string? videoPath = null;

            // Upload Image
            if (request.FeaturedImage != null)
            {
                imagePath = await _fileService.UploadNewsImageAsync(request.FeaturedImage);

                if (!string.IsNullOrEmpty(imagePath))
                {
                    var media = new Media
                    {
                        FileName = Path.GetFileName(imagePath),
                        OriginalFileName = request.FeaturedImage.FileName,
                        FilePath = imagePath,
                        FileType = Path.GetExtension(request.FeaturedImage.FileName),
                        ContentType = request.FeaturedImage.ContentType,
                        FileSize = request.FeaturedImage.Length,
                        UploadedBy = User.Identity?.Name ?? "Admin",
                        UploadedDate = DateTime.UtcNow,
                        IsActive = true
                    };

                    await _mediaRepository.AddAsync(media);
                }
            }

            // Upload Video
            if (request.FeaturedVideo != null)
            {
                videoPath = await _fileService.UploadNewsVideoAsync(request.FeaturedVideo);

                if (!string.IsNullOrEmpty(videoPath))
                {
                    var media = new Media
                    {
                        FileName = Path.GetFileName(videoPath),
                        OriginalFileName = request.FeaturedVideo.FileName,
                        FilePath = videoPath,
                        FileType = Path.GetExtension(request.FeaturedVideo.FileName),
                        ContentType = request.FeaturedVideo.ContentType,
                        FileSize = request.FeaturedVideo.Length,
                        UploadedBy = User.Identity?.Name ?? "Admin",
                        UploadedDate = DateTime.UtcNow,
                        IsActive = true
                    };

                    await _mediaRepository.AddAsync(media);
                }
            }

            var dto = new UpdateNewsDto
            {
                Id = id,
                Title = request.Title,
                Slug = request.Slug,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImage = imagePath,
                FeaturedVideo = videoPath,
                Author = request.Author,
                PublishDate = request.PublishDate,
                IsPublished = request.IsPublished,
                IsFeatured = request.IsFeatured,
                CategoryId = request.CategoryId
            };

            var updatedNews = await _newsService.UpdateAsync(dto);

            if (updatedNews == null)
            {
                return NotFound(new
                {
                    message = "News not found."
                });
            }

            return Ok(updatedNews);
        }
        // DELETE: api/News/5
        
        [HttpDelete("{id:int}")]
        [RequestSizeLimit(1610612736)]
        [RequestFormLimits(MultipartBodyLengthLimit = 1610612736)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _newsService.DeleteAsync(id);

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