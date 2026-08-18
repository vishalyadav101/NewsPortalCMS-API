using Microsoft.AspNetCore.Http;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface IWebsiteSettingFileService
    {
        Task<string?> UploadLogoAsync(IFormFile? file);

        Task<string?> UploadFaviconAsync(IFormFile? file);

        void DeleteFile(string? relativePath);
    }
}