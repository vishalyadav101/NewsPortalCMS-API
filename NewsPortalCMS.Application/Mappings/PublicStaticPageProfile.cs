using AutoMapper;
using NewsPortalCMS.Application.DTOs.Public;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Mappings;

public class PublicStaticPageProfile : Profile
{
    public PublicStaticPageProfile()
    {
        CreateMap<StaticPage, PublicStaticPageDto>();
    }
}