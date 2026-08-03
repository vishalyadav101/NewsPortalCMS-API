using AutoMapper;
using NewsPortalCMS.Application.DTOs.Public;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Mappings;

public class PublicMenuProfile : Profile
{
    public PublicMenuProfile()
    {
        CreateMap<Menu, PublicMenuDto>();

        CreateMap<MenuItem, PublicMenuItemDto>();
    }
}