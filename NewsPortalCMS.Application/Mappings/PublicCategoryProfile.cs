using AutoMapper;
using NewsPortalCMS.Application.DTOs.Public;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.MappingProfiles;

public class PublicMappingProfile : Profile
{
    public PublicMappingProfile()
    {
        CreateMap<Category, PublicCategoryDto>();
    }
}