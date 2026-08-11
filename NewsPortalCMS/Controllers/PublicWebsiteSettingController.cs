using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Controllers;

[ApiController]
[Route("api/public/website-settings")]
[AllowAnonymous]
public class PublicWebsiteSettingController : ControllerBase
{
    private readonly IPublicWebsiteSettingService _publicWebsiteSettingService;

    public PublicWebsiteSettingController(
        IPublicWebsiteSettingService publicWebsiteSettingService)
    {
        _publicWebsiteSettingService =
            publicWebsiteSettingService;
    }

    // GET: api/public/website-settings
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var settings =
            await _publicWebsiteSettingService.GetAsync();

        if (settings == null)
        {
            return NotFound(new
            {
                message = "Website settings not found."
            });
        }

        return Ok(settings);
    }
}