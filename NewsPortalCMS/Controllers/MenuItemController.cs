using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.MenuItem;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;

    public MenuItemController(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    [HttpGet("menu/{menuId}")]
    public async Task<IActionResult> GetByMenu(int menuId)
    {
        var items = await _menuItemService.GetAllByMenuIdAsync(menuId);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _menuItemService.GetByIdAsync(id);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMenuItemDto dto)
    {
        var item = await _menuItemService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateMenuItemDto dto)
    {
        var result = await _menuItemService.UpdateAsync(id, dto);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _menuItemService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}