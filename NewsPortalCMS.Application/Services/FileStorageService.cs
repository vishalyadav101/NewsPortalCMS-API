using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using NewsPortalCMS.Application.Interfaces.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;

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
        "media"
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
            {
                throw new ArgumentException(
                    "Invalid upload folder.");
            }
            if (file == null || file.Length == 0)
                return null;

            var webRootPath = _environment.WebRootPath;

            if (string.IsNullOrEmpty(webRootPath))
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

            var extension =
                Path.GetExtension(file.FileName)
                .ToLowerInvariant();

            var fileName =
                $"{Guid.NewGuid():N}{extension}";

            var filePath =
                Path.Combine(uploadFolder, fileName);

            await using var stream =
                new FileStream(
                    filePath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None);

            await file.CopyToAsync(
                stream,
                cancellationToken);

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

            // Convert URL path to Windows/Linux path
            var relativePath = fileUrl
                .TrimStart('/', '\\')
                .Replace('/', Path.DirectorySeparatorChar)
                .Replace('\\', Path.DirectorySeparatorChar);

            // Only allow files under wwwroot/uploads
            var uploadsRoot = Path.GetFullPath(
                Path.Combine(webRootPath, "uploads"));

            var fullPath = Path.GetFullPath(
                Path.Combine(webRootPath, relativePath));

            // Prevent path traversal
            if (!fullPath.StartsWith(
                    uploadsRoot + Path.DirectorySeparatorChar,
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
            var filePath = GetSafeFilePath(fileUrl);

            if (filePath == null)
                return Task.FromResult(false);

            if (!File.Exists(filePath))
                return Task.FromResult(false);

            File.Delete(filePath);

            return Task.FromResult(true);
        }

        public Task<bool> ExistsAsync(
      string fileUrl,
      CancellationToken cancellationToken = default)
        {
            var filePath = GetSafeFilePath(fileUrl);

            if (filePath == null)
                return Task.FromResult(false);

            return Task.FromResult(
                File.Exists(filePath));
        }
        public async Task<string?> SaveImageWithThumbnailAsync(
       IFormFile file,
       string folder,

       CancellationToken cancellationToken = default)
        {
            if (!IsValidFolder(folder))
            {
                throw new ArgumentException(
                    "Invalid upload folder.");
            }
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

            var thumbnailFolder = Path.Combine(
                uploadFolder,
                "thumbnails");

            Directory.CreateDirectory(uploadFolder);
            Directory.CreateDirectory(thumbnailFolder);

            var extension =
                Path.GetExtension(file.FileName)
                    .ToLowerInvariant();

            var fileName =
                $"{Guid.NewGuid():N}{extension}";

            var originalPath =
                Path.Combine(
                    uploadFolder,
                    fileName);

            var thumbnailFileName =
                $"{Path.GetFileNameWithoutExtension(fileName)}_thumb{extension}";

            var thumbnailPath =
                Path.Combine(
                    thumbnailFolder,
                    thumbnailFileName);

            try
            {
                // =====================================================
                // STEP 1: Save uploaded image
                // =====================================================

                await using (var stream = new FileStream(
                    originalPath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None))
                {
                    await file.CopyToAsync(
                        stream,
                        cancellationToken);
                }

                // =====================================================
                // STEP 2: Load image
                // =====================================================

                using (var image = await Image.LoadAsync(
                    originalPath,
                    cancellationToken))
                {
                    // =================================================
                    // STEP 3: Compress original image
                    // =================================================

                    switch (extension)
                    {
                        case ".jpg":
                        case ".jpeg":

                            await image.SaveAsync(
                                originalPath,
                                new JpegEncoder
                                {
                                    Quality = 75
                                },
                                cancellationToken);

                            break;

                        case ".webp":

                            await image.SaveAsync(
                                originalPath,
                                new WebpEncoder
                                {
                                    Quality = 75
                                },
                                cancellationToken);

                            break;

                        case ".png":

                            await image.SaveAsync(
                                originalPath,
                                new PngEncoder
                                {
                                    CompressionLevel =
                                        PngCompressionLevel.BestCompression
                                },
                                cancellationToken);

                            break;

                        default:

                            break;
                    }

                    // =================================================
                    // STEP 4: Generate thumbnail
                    // =================================================

                    using var thumbnail =
                        image.Clone(context =>
                        {
                            context.Resize(
                                new ResizeOptions
                                {
                                    Size = new Size(400, 300),
                                    Mode = ResizeMode.Max
                                });
                        });

                    switch (extension)
                    {
                        case ".jpg":
                        case ".jpeg":

                            await thumbnail.SaveAsync(
                                thumbnailPath,
                                new JpegEncoder
                                {
                                    Quality = 75
                                },
                                cancellationToken);

                            break;

                        case ".webp":

                            await thumbnail.SaveAsync(
                                thumbnailPath,
                                new WebpEncoder
                                {
                                    Quality = 75
                                },
                                cancellationToken);

                            break;

                        case ".png":

                            await thumbnail.SaveAsync(
                                thumbnailPath,
                                new PngEncoder
                                {
                                    CompressionLevel =
                                        PngCompressionLevel.BestCompression
                                },
                                cancellationToken);

                            break;

                        default:

                            await thumbnail.SaveAsync(
                                thumbnailPath,
                                cancellationToken);

                            break;
                    }
                }

                // =====================================================
                // STEP 5: Return URL only after everything succeeds
                // =====================================================

                return $"/uploads/{folder}/{fileName}";
            }
            catch
            {
                // =====================================================
                // CLEANUP
                // =====================================================

                try
                {
                    if (File.Exists(originalPath))
                    {
                        File.Delete(originalPath);
                    }
                }
                catch
                {
                    // Ignore cleanup error
                }

                try
                {
                    if (File.Exists(thumbnailPath))
                    {
                        File.Delete(thumbnailPath);
                    }
                }
                catch
                {
                    // Ignore cleanup error
                }

                throw;
            }
        }

        public Task<bool> DeleteWithThumbnailAsync(
    string fileUrl,
    CancellationToken cancellationToken = default)
        {
            var filePath = GetSafeFilePath(fileUrl);

            if (filePath == null)
                return Task.FromResult(false);

            var deleted = false;

            // Delete original file
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                deleted = true;
            }

            // Delete thumbnail
            var directory = Path.GetDirectoryName(filePath);

            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var extension = Path.GetExtension(filePath);

            if (!string.IsNullOrWhiteSpace(directory))
            {
                var thumbnailPath = Path.Combine(
                    directory,
                    "thumbnails",
                    $"{fileName}_thumb{extension}");

                if (File.Exists(thumbnailPath))
                {
                    File.Delete(thumbnailPath);
                    deleted = true;
                }
            }

            return Task.FromResult(deleted);
        }
    }

}