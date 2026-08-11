using NewsPortalCMS.Application.DTOs.PublicWebsiteSetting;

namespace NewsPortalCMS.Application.Interfaces.Services;

public interface IPublicWebsiteSettingService
{
    Task<PublicWebsiteSettingResponseDto?> GetAsync();
}