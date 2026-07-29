using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.StaticPage;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaticPagesController : ControllerBase
{
    private readonly IStaticPageService _staticPageService;

    public StaticPagesController(IStaticPageService staticPageService)
    {
        _staticPageService = staticPageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pages = await _staticPageService.GetAllAsync();
        return Ok(pages);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var page = await _staticPageService.GetByIdAsync(id);

        if (page == null)
            return NotFound();

        return Ok(page);
    }

    [HttpGet("slug/{slug}")]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var page = await _staticPageService.GetBySlugAsync(slug);

        if (page == null)
            return NotFound();

        return Ok(page);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStaticPageDto dto)
    {
        var page = await _staticPageService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = page.Id }, page);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateStaticPageDto dto)
    {
        var updated = await _staticPageService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _staticPageService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}