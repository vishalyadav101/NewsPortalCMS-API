using AutoMapper;
using NewsPortalCMS.Application.DTOs.Advertisement;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Domain.Enums;

namespace NewsPortalCMS.Application.MappingProfiles
{
    public class AdvertisementProfile : Profile
    {
        public AdvertisementProfile()
        {
            CreateMap<CreateAdvertisementDto, Advertisement>();

            CreateMap<UpdateAdvertisementDto, Advertisement>();

            CreateMap<Advertisement, AdvertisementResponseDto>()
                .ForMember(
                    dest => dest.PositionName,
                    opt => opt.MapFrom(src => GetPositionName(src.Position))
                );
        }

        private static string GetPositionName(AdvertisementPosition position)
        {
            return position switch
            {
                AdvertisementPosition.TopBanner => "Top Banner",
                AdvertisementPosition.Sidebar => "Sidebar",
                AdvertisementPosition.Footer => "Footer",
                _ => "Unknown"
            };
        }
    }
}