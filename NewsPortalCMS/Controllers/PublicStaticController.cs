using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.API.Controllers;

[Route("api/public/pages")]
[ApiController]
public class PublicStaticPageController : ControllerBase
{
    private readonly IPublicStaticPageService _publicStaticPageService;

    public PublicStaticPageController(
        IPublicStaticPageService publicStaticPageService)
    {
        _publicStaticPageService = publicStaticPageService;
    }


    // GET: api/public/pages
    [HttpGet]
    public async Task<IActionResult> GetActivePages()
    {
        var pages = await _publicStaticPageService.GetActivePagesAsync();

        return Ok(pages);
    }


    // GET: api/public/pages/{slug}
    [HttpGet("{slug}")]
    public async Task<IActionResult> GetPageBySlug(string slug)
    {
        var page = await _publicStaticPageService.GetPageBySlugAsync(slug);

        if (page == null)
        {
            return NotFound(new
            {
                message = "Page not found"
            });
        }

        return Ok(page);
    }
}