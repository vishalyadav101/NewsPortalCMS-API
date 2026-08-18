using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Infrastructure.Services
{
    public class WebsiteSettingFileService : IWebsiteSettingFileService
    {
        private readonly IWebHostEnvironment _environment;

        public WebsiteSettingFileService(
            IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string?> UploadLogoAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            var allowedExtensions = new[]
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".webp",
                ".svg"
            };

            return await UploadFileAsync(
                file,
                "logo",
                allowedExtensions);
        }

        public async Task<string?> UploadFaviconAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            var allowedExtensions = new[]
            {
                ".ico",
                ".png",
                ".jpg",
                ".jpeg",
                ".svg"
            };

            return await UploadFileAsync(
                file,
                "favicon",
                allowedExtensions);
        }

        private async Task<string> UploadFileAsync(
            IFormFile file,
            string folderName,
            string[] allowedExtensions)
        {
            var extension = Path
                .GetExtension(file.FileName)
                .ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                throw new Exception(
                    $"Invalid {folderName} file type.");
            }

            // Maximum size = 10 MB
            if (file.Length > 10L * 1024 * 1024)
            {
                throw new Exception(
                    $"{folderName} size cannot exceed 10 MB.");
            }

            var webRootPath = _environment.WebRootPath;

            if (string.IsNullOrEmpty(webRootPath))
            {
                webRootPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot");
            }

            var uploadFolder = Path.Combine(
                webRootPath,
                "uploads",
                "website",
                folderName);

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var fileName = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(
                uploadFolder,
                fileName);

            await using var stream = new FileStream(
                filePath,
                FileMode.Create);

            await file.CopyToAsync(stream);

            return "/" + Path.Combine(
                "uploads",
                "website",
                folderName,
                fileName)
                .Replace("\\", "/");
        }

        public void DeleteFile(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return;

            var webRootPath = _environment.WebRootPath;

            if (string.IsNullOrEmpty(webRootPath))
            {
                webRootPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot");
            }

            relativePath = relativePath.TrimStart(
                '/',
                '\\');

            var fullPath = Path.Combine(
                webRootPath,
                relativePath.Replace(
                    "/",
                    Path.DirectorySeparatorChar.ToString()));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}