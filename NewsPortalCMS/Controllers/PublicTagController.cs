using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.Interfaces;

namespace NewsPortalCMS.Controllers;

[ApiController]
[Route("api/public/tags")]
[AllowAnonymous]
public class PublicTagController : ControllerBase
{
    private readonly IPublicTagService _publicTagService;

    public PublicTagController(
        IPublicTagService publicTagService)
    {
        _publicTagService = publicTagService;
    }

    // GET: api/public/tags
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tags = await _publicTagService.GetAllAsync();

        return Ok(tags);
    }

    // GET: api/public/tags/{slug}
    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug(string slug)
    {
        var tag = await _publicTagService.GetBySlugAsync(slug);

        if (tag == null)
        {
            return NotFound(new
            {
                message = "Tag not found."
            });
        }

        return Ok(tag);
    }
}