using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.DTOs.News;
using NewsPortalCMS.Services.Interfaces;

namespace NewsPortalCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        // GET: api/News
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var news = await _newsService.GetAllAsync();

            return Ok(news);
        }

        // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var news = await _newsService.GetByIdAsync(id);

            if (news == null)
                return NotFound(new { message = "News not found." });

            return Ok(news);
        }

        // POST: api/News
        [HttpPost]
        public async Task<IActionResult> Create(CreateNewsDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdNews = await _newsService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdNews.Id },
                createdNews);
        }

        // PUT: api/News/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateNewsDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { message = "Id mismatch." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedNews = await _newsService.UpdateAsync(dto);

            if (updatedNews == null)
                return NotFound(new { message = "News not found." });

            return Ok(updatedNews);
        }

        // DELETE: api/News/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _newsService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "News not found." });

            return Ok(new
            {
                message = "News deleted successfully."
            });
        }
    }
}