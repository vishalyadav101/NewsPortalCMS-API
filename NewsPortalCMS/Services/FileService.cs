using NewsPortalCMS.Application.Interfaces;

namespace NewsPortalCMS.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // =========================================================
        // NEWS IMAGE
        // =========================================================

        public async Task<string?> UploadNewsImageAsync(IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

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

            var extension = Path
                .GetExtension(file.FileName)
                .ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException(
                    "Invalid image file type.");
            }

            // Maximum image/document size = 10 MB
            if (file.Length > 10L * 1024 * 1024)
            {
                throw new InvalidOperationException(
                    "File size cannot exceed 10 MB.");
            }

            return await SaveFileAsync(
                file,
                "uploads/news",
                allowedExtensions);
        }


        // =========================================================
        // NEWS VIDEO
        // =========================================================

        public async Task<string?> UploadNewsVideoAsync(
            IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            string[] allowedExtensions =
            {
                ".mp4",
                ".avi",
                ".mov",
                ".wmv",
                ".mkv",
                ".webm"
            };

            var extension = Path
                .GetExtension(file.FileName)
                .ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException(
                    "Invalid video file type.");
            }

            // Maximum video size = 1 GB
            if (file.Length > 1L * 1024 * 1024 * 1024)
            {
                throw new InvalidOperationException(
                    "Video size cannot exceed 1 GB.");
            }

            return await SaveFileAsync(
                file,
                "uploads/videos",
                allowedExtensions);
        }


        // =========================================================
        // WEBSITE LOGO
        // =========================================================

        public async Task<string?> UploadWebsiteLogoAsync(
            IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            string[] allowedExtensions =
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".webp",
                ".svg"
            };

            var extension = Path
                .GetExtension(file.FileName)
                .ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException(
                    "Invalid logo file type. Allowed formats: JPG, JPEG, PNG, WEBP and SVG.");
            }

            // Logo maximum size = 5 MB
            if (file.Length > 5L * 1024 * 1024)
            {
                throw new InvalidOperationException(
                    "Logo size cannot exceed 5 MB.");
            }

            return await SaveFileAsync(
                file,
                "uploads/website",
                allowedExtensions);
        }


        // =========================================================
        // WEBSITE FAVICON
        // =========================================================

        public async Task<string?> UploadWebsiteFaviconAsync(
            IFormFile? file)
        {
            if (file == null || file.Length == 0)
                return null;

            string[] allowedExtensions =
            {
                ".ico",
                ".png",
                ".jpg",
                ".jpeg",
                ".webp"
            };

            var extension = Path
                .GetExtension(file.FileName)
                .ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException(
                    "Invalid favicon file type. Allowed formats: ICO, PNG, JPG, JPEG and WEBP.");
            }

            // Favicon maximum size = 2 MB
            if (file.Length > 2L * 1024 * 1024)
            {
                throw new InvalidOperationException(
                    "Favicon size cannot exceed 2 MB.");
            }

            return await SaveFileAsync(
                file,
                "uploads/website",
                allowedExtensions);
        }


        // =========================================================
        // COMMON FILE SAVE METHOD
        // =========================================================

        private async Task<string> SaveFileAsync(
            IFormFile file,
            string relativeFolder,
            string[] allowedExtensions)
        {
            var webRootPath = _environment.WebRootPath;

            if (string.IsNullOrEmpty(webRootPath))
            {
                webRootPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot");
            }

            var uploadFolder = Path.Combine(
                webRootPath,
                relativeFolder.Replace(
                    "/",
                    Path.DirectorySeparatorChar.ToString()));

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var extension = Path
                .GetExtension(file.FileName)
                .ToLowerInvariant();

            var fileName =
                $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(
                uploadFolder,
                fileName);

            await using var stream =
                new FileStream(
                    filePath,
                    FileMode.Create);

            await file.CopyToAsync(stream);

            return "/" +
                   Path.Combine(
                       relativeFolder,
                       fileName)
                   .Replace("\\", "/");
        }


        // =========================================================
        // DELETE FILE
        // =========================================================

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

            var cleanPath = relativePath.TrimStart(
                '/',
                '\\');

            var fullPath = Path.Combine(
                webRootPath,
                cleanPath.Replace(
                    "/",
                    Path.DirectorySeparatorChar.ToString()));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
