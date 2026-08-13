using NewsPortalCMS.Application.DTOs.WebsiteSetting;

namespace NewsPortalCMS.Application.Interfaces.Services;

public interface IWebsiteSettingService
{
    Task<WebsiteSettingResponseDto?> GetAsync();

    Task<WebsiteSettingResponseDto?> GetByIdAsync(int id);

    Task<WebsiteSettingResponseDto> CreateAsync(
        WebsiteSettingCreateDto model);

    Task<bool> UpdateAsync(
        int id,
        WebsiteSettingUpdateDto model);

    Task<bool> DeleteAsync(int id);
}