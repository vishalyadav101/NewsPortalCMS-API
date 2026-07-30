using AutoMapper;
using NewsPortalCMS.Application.DTOs.Menu;
using NewsPortalCMS.Application.DTOs.MenuItem;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Mappings;

public class MenuProfile : Profile
{
    public MenuProfile()
    {
        CreateMap<Menu, MenuResponseDto>();
        CreateMap<CreateMenuDto, Menu>();
        CreateMap<UpdateMenuDto, Menu>();

        CreateMap<MenuItem, MenuItemResponseDto>();
        CreateMap<CreateMenuItemDto, MenuItem>();
        CreateMap<UpdateMenuItemDto, MenuItem>();
    }
}