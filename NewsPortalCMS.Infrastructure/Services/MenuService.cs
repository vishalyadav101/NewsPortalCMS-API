using AutoMapper;
using NewsPortalCMS.Application.DTOs.Menu;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _repository;
    private readonly IMapper _mapper;

    public MenuService(
        IMenuRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MenuResponseDto>> GetAllAsync()
    {
        var menus = await _repository.GetAllAsync();

        return _mapper.Map<IEnumerable<MenuResponseDto>>(menus);
    }

    public async Task<MenuResponseDto?> GetByIdAsync(int id)
    {
        var menu = await _repository.GetByIdAsync(id);

        if (menu == null)
            return null;

        return _mapper.Map<MenuResponseDto>(menu);
    }

    public async Task<MenuResponseDto> CreateAsync(CreateMenuDto dto)
    {
        var menu = _mapper.Map<Menu>(dto);

        menu.CreatedDate = DateTime.UtcNow;

        await _repository.AddAsync(menu);

        return _mapper.Map<MenuResponseDto>(menu);
    }

    public async Task<bool> UpdateAsync(int id, UpdateMenuDto dto)
    {
        var menu = await _repository.GetByIdAsync(id);

        if (menu == null)
            return false;

        _mapper.Map(dto, menu);

        menu.UpdatedDate = DateTime.UtcNow;

        await _repository.UpdateAsync(menu);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var menu = await _repository.GetByIdAsync(id);

        if (menu == null)
            return false;

        await _repository.DeleteAsync(id);

        return true;
    }
}