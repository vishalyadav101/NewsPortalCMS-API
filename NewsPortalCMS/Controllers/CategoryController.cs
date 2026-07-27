using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.Category;
using NewsPortalCMS.Application.Interfaces;

namespace NewsPortalCMS.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET: api/Category
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();

        return Ok(categories);
    }

    // GET: api/Category/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
        {
            return NotFound("Category not found.");
        }

        return Ok(category);
    }

    // POST: api/Category
    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateDto model)
    {
        try
        {
            var category = await _categoryService.CreateAsync(model);

            return CreatedAtAction(
                nameof(GetById),
                new { id = category.Id },
                category);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/Category/1
     
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
    int id,
    CategoryUpdateDto model)
    {
        try
        {
            var result = await _categoryService.UpdateAsync(id, model);

            if (!result)
            {
                return NotFound("Category not found.");
            }

            return Ok("Category updated successfully.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Category/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _categoryService.DeleteAsync(id);

        if (!result)
        {
            return NotFound("Category not found.");
        }

        return Ok("Category deleted successfully.");
    }
}