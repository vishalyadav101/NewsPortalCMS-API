using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.API.Controllers;

[Route("api/public/menu")]
[ApiController]
public class PublicMenuController : ControllerBase
{
    private readonly IPublicMenuService _publicMenuService;

    public PublicMenuController(IPublicMenuService publicMenuService)
    {
        _publicMenuService = publicMenuService;
    }


    // GET: api/public/menu
    [HttpGet]
    public async Task<IActionResult> GetActiveMenus()
    {
        var menus = await _publicMenuService.GetActiveMenusAsync();

        return Ok(menus);
    }


    // GET: api/public/menu/{location}
    [HttpGet("{location}")]
    public async Task<IActionResult> GetMenuByLocation(string location)
    {
        var menu = await _publicMenuService.GetMenuByLocationAsync(location);

        if (menu == null)
            return NotFound(new
            {
                message = "Menu not found"
            });

        return Ok(menu);
    }
}