using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.Tag;
using NewsPortalCMS.Application.Interfaces;

namespace NewsPortalCMS.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    // GET: api/Tag
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tags = await _tagService.GetAllAsync();

        return Ok(tags);
    }

    // GET: api/Tag/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tag = await _tagService.GetByIdAsync(id);

        if (tag == null)
        {
            return NotFound("Tag not found.");
        }

        return Ok(tag);
    }

    // POST: api/Tag
    [HttpPost]
    public async Task<IActionResult> Create(
        TagCreateDto model)
    {
        try
        {
            var tag = await _tagService.CreateAsync(model);

            return CreatedAtAction(
                nameof(GetById),
                new { id = tag.Id },
                tag);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/Tag/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        TagUpdateDto model)
    {
        try
        {
            var result =
                await _tagService.UpdateAsync(id, model);

            if (!result)
            {
                return NotFound("Tag not found.");
            }

            return Ok("Tag updated successfully.");
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Tag/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _tagService.DeleteAsync(id);

        if (!result)
        {
            return NotFound("Tag not found.");
        }

        return Ok("Tag deleted successfully.");
    }
}