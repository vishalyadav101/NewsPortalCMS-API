using AutoMapper;
using NewsPortalCMS.Application.DTOs.MenuItem;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _repository;
    private readonly IMapper _mapper;

    public MenuItemService(
        IMenuItemRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MenuItemResponseDto>> GetAllByMenuIdAsync(int menuId)
    {
        var items = await _repository.GetAllByMenuIdAsync(menuId);

        return _mapper.Map<IEnumerable<MenuItemResponseDto>>(items);
    }

    public async Task<MenuItemResponseDto?> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item == null)
            return null;

        return _mapper.Map<MenuItemResponseDto>(item);
    }

    public async Task<MenuItemResponseDto> CreateAsync(CreateMenuItemDto dto)
    {
        var item = _mapper.Map<MenuItem>(dto);

        item.CreatedDate = DateTime.UtcNow;

        await _repository.AddAsync(item);

        return _mapper.Map<MenuItemResponseDto>(item);
    }

    public async Task<bool> UpdateAsync(int id, UpdateMenuItemDto dto)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item == null)
            return false;

        _mapper.Map(dto, item);

        item.UpdatedDate = DateTime.UtcNow;

        await _repository.UpdateAsync(item);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item == null)
            return false;

        await _repository.DeleteAsync(id);

        return true;
    }
}