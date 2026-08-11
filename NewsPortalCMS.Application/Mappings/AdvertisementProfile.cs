using AutoMapper;
using NewsPortalCMS.Application.DTOs.Advertisement;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.MappingProfiles
{
    public class AdvertisementProfile : Profile
    {
        public AdvertisementProfile()
        {
            // Entity -> Response
            CreateMap<Advertisement, AdvertisementResponseDto>();
        }
    }
}