using AutoMapper;
using NewsPortalCMS.Application.DTOs.PublicWebsiteSetting;
using NewsPortalCMS.Application.DTOs.WebsiteSetting;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Mappings;

public class WebsiteSettingProfile : Profile
{
    public WebsiteSettingProfile()
    {
        CreateMap<WebsiteSettingCreateDto, WebsiteSetting>();

        CreateMap<WebsiteSettingUpdateDto, WebsiteSetting>();

        CreateMap<WebsiteSetting, WebsiteSettingResponseDto>();

        CreateMap<WebsiteSetting, PublicWebsiteSettingResponseDto>();
    }
}