using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.SubCategory;
using NewsPortalCMS.Application.Interfaces;

namespace NewsPortalCMS.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SubCategoryController : ControllerBase
{
    private readonly ISubCategoryService _subCategoryService;

    public SubCategoryController(
        ISubCategoryService subCategoryService)
    {
        _subCategoryService = subCategoryService;
    }

    // GET: api/SubCategory
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var subCategories =
            await _subCategoryService.GetAllAsync();

        return Ok(subCategories);
    }

    // GET: api/SubCategory/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var subCategory =
            await _subCategoryService.GetByIdAsync(id);

        if (subCategory == null)
        {
            return NotFound("SubCategory not found.");
        }

        return Ok(subCategory);
    }

    // POST: api/SubCategory
    [HttpPost]
    public async Task<IActionResult> Create(
        SubCategoryCreateDto model)
    {
        try
        {
            var subCategory =
                await _subCategoryService.CreateAsync(model);

            return CreatedAtAction(
                nameof(GetById),
                new { id = subCategory.Id },
                subCategory);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/SubCategory/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        SubCategoryUpdateDto model)
    {
        try
        {
            var result =
                await _subCategoryService.UpdateAsync(id, model);

            if (!result)
            {
                return NotFound("SubCategory not found.");
            }

            return Ok("SubCategory updated successfully.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/SubCategory/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result =
            await _subCategoryService.DeleteAsync(id);

        if (!result)
        {
            return NotFound("SubCategory not found.");
        }

        return Ok("SubCategory deleted successfully.");
    }
}