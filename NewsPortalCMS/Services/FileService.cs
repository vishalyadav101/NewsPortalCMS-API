using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace NewsPortalCMS.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string?> UploadNewsImageAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            // Allowed file extensions
            string[] allowedExtensions =
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".gif",
                ".webp",
                ".bmp",
                ".pdf"
            };

            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                throw new Exception("Invalid file type.");
            }

            // Maximum file size (10 MB)
            if (file.Length > 10 * 1024 * 1024)
            {
                throw new Exception("File size cannot exceed 10 MB.");
            }

            // Create uploads/news folder if it doesn't exist
            var webRootPath = _environment.WebRootPath;

            if (string.IsNullOrEmpty(webRootPath))
            {
                webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            var uploadFolder = Path.Combine(
                webRootPath,
                "uploads",
                "news");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Generate unique filename
            var fileName = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path for database
            return "/" + Path.Combine("uploads", "news", fileName)
                .Replace("\\", "/");
        }

        public async Task<string?> UploadNewsVideoAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            // Allowed video extensions
            string[] allowedExtensions =
            {
        ".mp4",
        ".avi",
        ".mov",
        ".wmv",
        ".mkv",
        ".webm"
    };

            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                throw new Exception("Invalid video file type.");
            }

            // Maximum file size (100 MB)
            if (file.Length > 100 * 1024 * 1024)
            {
                throw new Exception("Video size cannot exceed 100 MB.");
            }

            var webRootPath = _environment.WebRootPath;

            if (string.IsNullOrEmpty(webRootPath))
            {
                webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            // Create uploads/videos folder
            var uploadFolder = Path.Combine(
                webRootPath,
                "uploads",
                "videos");

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Generate unique filename
            var fileName = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path
            return "/" + Path.Combine("uploads", "videos", fileName)
                .Replace("\\", "/");
        }
        public void DeleteFile(string? relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return;

            var webRootPath = _environment.WebRootPath;

            if (string.IsNullOrEmpty(webRootPath))
            {
                webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            var fullPath = Path.Combine(
                webRootPath,
                relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}