using NewsPortalCMS.Application.DTOs.Menu;

namespace NewsPortalCMS.Application.Interfaces.Services;

public interface IMenuService
{
    Task<MenuResponseDto> CreateAsync(CreateMenuDto dto);

    Task<IEnumerable<MenuResponseDto>> GetAllAsync();

    Task<MenuResponseDto?> GetByIdAsync(int id);

    Task<bool> UpdateAsync(int id, UpdateMenuDto dto);

    Task<bool> DeleteAsync(int id);
}