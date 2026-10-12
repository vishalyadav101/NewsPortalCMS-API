using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Infrastructure.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;

        public FileStorageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        private static readonly HashSet<string> AllowedFolders =
            new(StringComparer.OrdinalIgnoreCase)
            {
                "news",
                "videos",
                "advertisements",
                "documents",
                "media",
                "profiles"
            };

        private static bool IsValidFolder(string folder)
        {
            return !string.IsNullOrWhiteSpace(folder)
                   && AllowedFolders.Contains(folder);
        }

        public async Task<string?> SaveAsync(
            IFormFile file,
            string folder,
            CancellationToken cancellationToken = default)
        {
            if (!IsValidFolder(folder))
                throw new ArgumentException("Invalid upload folder.");

            if (file == null || file.Length == 0)
                return null;

            var webRootPath = _environment.WebRootPath;

            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                webRootPath = Path.Combine(
                    _environment.ContentRootPath,
                    "wwwroot");
            }

            var uploadFolder = Path.Combine(
                webRootPath,
                "uploads",
                folder);

            Directory.CreateDirectory(uploadFolder);

            var extension = Path.GetExtension(file.FileName)
                .ToLowerInvariant();

            var fileName = $"{Guid.NewGuid():N}{extension}";
            var filePath = Path.Combine(uploadFolder, fileName);

            await using var stream = new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None);

            await file.CopyToAsync(stream, cancellationToken);

            return $"/uploads/{folder}/{fileName}";
        }

        private string? GetSafeFilePath(string fileUrl)
        {
            if (string.IsNullOrWhiteSpace(fileUrl))
                return null;

            var webRootPath = _environment.WebRootPath;

            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                webRootPath = Path.Combine(
                    _environment.ContentRootPath,
                    "wwwroot");
            }

            var relativePath = fileUrl
                .TrimStart('/', '\\')
                .Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar);

            var uploadsRoot = Path.GetFullPath(
                Path.Combine(webRootPath, "uploads"));

            var fullPath = Path.GetFullPath(
                Path.Combine(webRootPath, relativePath));

            var safeRoot = uploadsRoot + Path.DirectorySeparatorChar;

            if (!fullPath.StartsWith(
                    safeRoot,
                    StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return fullPath;
        }

        public Task<bool> DeleteAsync(
            string fileUrl,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var filePath = GetSafeFilePath(fileUrl);

            if (filePath == null || !File.Exists(filePath))
                return Task.FromResult(false);

            File.Delete(filePath);

            return Task.FromResult(true);
        }

        public Task<bool> ExistsAsync(
            string fileUrl,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var filePath = GetSafeFilePath(fileUrl);

            return Task.FromResult(
                filePath != null && File.Exists(filePath));
        }
    }
}