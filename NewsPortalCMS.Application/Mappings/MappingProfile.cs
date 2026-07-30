using AutoMapper;
using NewsPortalCMS.Application.DTOs.StaticPage;
using NewsPortalCMS.Domain.Entities;


namespace NewsPortalCMS.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // StaticPage Mappings

        CreateMap<CreateStaticPageDto, StaticPage>();

        CreateMap<UpdateStaticPageDto, StaticPage>();

        CreateMap<StaticPage, StaticPageResponseDto>();
    }
}
