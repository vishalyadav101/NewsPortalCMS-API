using AutoMapper;
using NewsPortalCMS.Application.DTOs.Seo;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Mappings
{
    public class SeoProfile : Profile
    {
        public SeoProfile()
        {
            // Entity -> Response DTO
            CreateMap<Seo, SeoResponseDto>();

            // Create DTO -> Entity
            CreateMap<CreateSeoDto, Seo>();

            // Update DTO -> Entity
            CreateMap<UpdateSeoDto, Seo>();
        }
    }
}