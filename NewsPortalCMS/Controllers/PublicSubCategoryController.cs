using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.Public;
using NewsPortalCMS.Application.Interfaces;

namespace NewsPortalCMS.API.Controllers
{
    [ApiController]
    [Route("api/publicsubcategories")]
    public class PublicSubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public PublicSubCategoryController(
            ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        // ============================================================
        // GET SUBCATEGORIES BY CATEGORY
        // GET /api/publicsubcategories/category/2
        // ============================================================

        [HttpGet("category/{categoryId:int}")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetByCategory(
            int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest(new
                {
                    message = "Invalid category id."
                });
            }

            var subCategories =
                await _subCategoryService
                    .GetAllAsync();

            var result =
                subCategories
                    .Where(x =>
                        x.CategoryId == categoryId &&
                        x.IsActive)
                    .OrderBy(x => x.DisplayOrder)
                    .ThenBy(x => x.Name)
                    .Select(x => new PublicSubCategoryDto
                    {
                        Id = x.Id,
                        CategoryId = x.CategoryId,
                        CategoryName = x.CategoryName,
                        Name = x.Name,
                        Slug = x.Slug,
                        Description = x.Description,
                        DisplayOrder = x.DisplayOrder
                    })
                    .ToList();

            return Ok(result);
        }
    }
}