using AutoMapper;
using NewsPortalCMS.Application.DTOs.Public;
using NewsPortalCMS.Entities;

namespace NewsPortalCMS.Application.Mappings
{
    public class PublicNewsProfile : Profile
    {
        public PublicNewsProfile()
        {
            CreateMap<News, PublicNewsDto>()
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src =>
                        src.Category != null
                            ? src.Category.Name
                            : string.Empty));

            CreateMap<News, PublicNewsDetailsDto>()
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src =>
                        src.Category != null
                            ? src.Category.Name
                            : string.Empty))
                .ForMember(
                    dest => dest.Tags,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Comments,
                    opt => opt.Ignore());
        }
    }
}