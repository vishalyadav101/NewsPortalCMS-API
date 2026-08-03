using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.API.Controllers.Public
{
    [ApiController]
    [Route("api/publiccategories")]
    public class PublicCategoryController : ControllerBase
    {
        private readonly IPublicCategoryService _publicCategoryService;

        public PublicCategoryController(IPublicCategoryService publicCategoryService)
        {
            _publicCategoryService = publicCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _publicCategoryService.GetActiveCategoriesAsync();

            return Ok(result);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetCategoryBySlug(string slug)
        {
            var result = await _publicCategoryService.GetCategoryBySlugAsync(slug);

            if (result == null)
            {
                return NotFound(new
                {
                    Message = "Category not found."
                });
            }

            return Ok(result);
        }
    }
}