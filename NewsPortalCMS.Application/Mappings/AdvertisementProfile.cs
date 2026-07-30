using AutoMapper;
using NewsPortalCMS.Application.DTOs.Advertisement;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.MappingProfiles
{
    public class AdvertisementProfile : Profile
    {
        public AdvertisementProfile()
        {
            CreateMap<CreateAdvertisementDto, Advertisement>();

            CreateMap<UpdateAdvertisementDto, Advertisement>();

            CreateMap<Advertisement, AdvertisementResponseDto>();
        }
    }
}