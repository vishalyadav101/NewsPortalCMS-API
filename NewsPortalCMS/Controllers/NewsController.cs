using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.DTOs.News;
using NewsPortalCMS.Models.News;
using NewsPortalCMS.Services;
using NewsPortalCMS.Services.Interfaces;

namespace NewsPortalCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly FileService _fileService;

        public NewsController(
            INewsService newsService,
            FileService fileService)
        {
            _newsService = newsService;
            _fileService = fileService;
        }

        // GET: api/News
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var news = await _newsService.GetAllAsync();
            return Ok(news);
        }

        // GET: api/News/5
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

        // POST: api/News
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateNewsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? imagePath = await _fileService.UploadNewsImageAsync(request.FeaturedImage);

            var dto = new CreateNewsDto
            {
                Title = request.Title,
                Slug = request.Slug,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImage = imagePath,
                Author = request.Author,
                PublishDate = request.PublishDate,
                IsPublished = request.IsPublished,
                IsFeatured = request.IsFeatured,
                CategoryId = request.CategoryId
            };

            var createdNews = await _newsService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdNews.Id },
                createdNews);
        }

        // PUT: api/News/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateNewsDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dto.Id = id;

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