using NewsPortalCMS.Application.DTOs.MenuItem;

namespace NewsPortalCMS.Application.Interfaces.Services;

public interface IMenuItemService
{
    Task<MenuItemResponseDto> CreateAsync(CreateMenuItemDto dto);

    Task<IEnumerable<MenuItemResponseDto>> GetAllByMenuIdAsync(int menuId);

    Task<MenuItemResponseDto?> GetByIdAsync(int id);

    Task<bool> UpdateAsync(int id, UpdateMenuItemDto dto);

    Task<bool> DeleteAsync(int id);
}