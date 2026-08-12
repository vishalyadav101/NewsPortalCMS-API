using Microsoft.AspNetCore.Http;
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


    // Logo Upload
    Task<string?> UploadLogoAsync(
        int id,
        IFormFile file);


    // Favicon Upload
    Task<string?> UploadFaviconAsync(
        int id,
        IFormFile file);
}