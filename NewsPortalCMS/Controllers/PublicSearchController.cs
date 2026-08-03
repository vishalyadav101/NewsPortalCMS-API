using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.API.Controllers.Public
{
    [Route("api/public/search")]
    [ApiController]
    public class PublicSearchController : ControllerBase
    {
        private readonly IPublicSearchService _searchService;

        public PublicSearchController(IPublicSearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(
            [FromQuery] string keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _searchService.SearchNewsAsync(
                keyword,
                pageNumber,
                pageSize);

            return Ok(result);
        }
    }
}