namespace NewsPortalCMS.Services
{
    public interface IFileService
    {
        Task<string?> UploadNewsImageAsync(IFormFile? file);

        Task<string?> UploadNewsVideoAsync(IFormFile? file);

        Task<string?> UploadWebsiteLogoAsync(IFormFile? file);

        Task<string?> UploadWebsiteFaviconAsync(IFormFile? file);

        void DeleteFile(string? relativePath);
    }
}
