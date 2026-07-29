using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.NewsTag;
using NewsPortalCMS.Application.Interfaces;

namespace NewsPortalCMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsTagController : ControllerBase
    {
        private readonly INewsTagService _newsTagService;

        public NewsTagController(INewsTagService newsTagService)
        {
            _newsTagService = newsTagService;
        }

        [HttpGet("{newsId:int}")]
        public async Task<IActionResult> GetByNewsId(int newsId)
        {
            var tagIds = await _newsTagService.GetTagIdsByNewsIdAsync(newsId);

            return Ok(tagIds);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignTags(
            [FromBody] AssignNewsTagsDto dto)
        {
            await _newsTagService.AssignTagsAsync(dto);

            return Ok(new
            {
                message = "Tags assigned successfully."
            });
        }
    }
}