using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.Menu;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var menus = await _menuService.GetAllAsync();
        return Ok(menus);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var menu = await _menuService.GetByIdAsync(id);

        if (menu == null)
            return NotFound();

        return Ok(menu);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMenuDto dto)
    {
        var menu = await _menuService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = menu.Id }, menu);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateMenuDto dto)
    {
        var result = await _menuService.UpdateAsync(id, dto);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _menuService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}