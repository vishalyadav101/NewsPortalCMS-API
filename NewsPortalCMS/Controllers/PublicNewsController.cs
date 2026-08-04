using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.API.Controllers.Public
{
    [ApiController]
    [Route("api/publicnews")]
    public class PublicNewsController : ControllerBase
    {
        private readonly IPublicNewsService _publicNewsService;

        public PublicNewsController(IPublicNewsService publicNewsService)
        {
            _publicNewsService = publicNewsService;
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestNews([FromQuery] int count = 10)
        {
            var result = await _publicNewsService.GetLatestNewsAsync(count);

            return Ok(result);
        }

        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedNews([FromQuery] int count = 10)
        {
            var result = await _publicNewsService.GetFeaturedNewsAsync(count);

            return Ok(result);
        }

        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularNews([FromQuery] int count = 10)
        {
            var result = await _publicNewsService.GetPopularNewsAsync(count);

            return Ok(result);
        }

        [HttpGet("category/{categoryId:int}")]
        public async Task<IActionResult> GetNewsByCategory(int categoryId)
        {
            var result = await _publicNewsService.GetNewsByCategoryAsync(categoryId);

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchNews([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest("Keyword is required.");
            }

            var result = await _publicNewsService.SearchNewsAsync(keyword);

            return Ok(result);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetNewsBySlug(string slug)
        {
            var result = await _publicNewsService.GetNewsBySlugAsync(slug);

            if (result == null)
            {
                return NotFound("News not found.");
            }

            return Ok(result);
        }
    }
}