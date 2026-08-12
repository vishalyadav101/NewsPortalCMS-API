using Microsoft.AspNetCore.Http;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Infrastructure.Services
{
    public class FileValidationService : IFileValidationService
    {
        private static readonly HashSet<string> ImageExtensions =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".gif",
                ".webp",
                ".bmp"
            };

        private static readonly HashSet<string> VideoExtensions =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ".mp4",
                ".webm",
                ".mov",
                ".avi",
                ".mkv"
            };

        private static readonly HashSet<string> DocumentExtensions =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ".pdf",
                ".doc",
                ".docx"
            };

        public void ValidateImage(IFormFile file)
        {
            Validate(
                file,
                ImageExtensions,
                10L * 1024 * 1024,
                "Image");
        }

        public void ValidateVideo(IFormFile file)
        {
            Validate(
                file,
                VideoExtensions,
                1L * 1024 * 1024 * 1024,
                "Video");
        }

        public void ValidateDocument(IFormFile file)
        {
            Validate(
                file,
                DocumentExtensions,
                20L * 1024 * 1024,
                "Document");
        }

        private static void Validate(
            IFormFile file,
            HashSet<string> allowedExtensions,
            long maxSize,
            string fileType)
        {
            // =====================================================
            // 1. Check file
            // =====================================================

            if (file == null)
            {
                throw new ArgumentException(
                    $"{fileType} file is required.");
            }

            if (file.Length == 0)
            {
                throw new ArgumentException(
                    $"{fileType} file is empty.");
            }

            // =====================================================
            // 2. Get extension safely
            // =====================================================

            var extension = Path
                .GetExtension(file.FileName)
                .Trim()
                .ToLowerInvariant();

            // =====================================================
            // 3. Validate extension
            // =====================================================

            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentException(
                    $"{fileType} file must have a valid extension.");
            }

            if (!allowedExtensions.Contains(extension))
            {
                throw new ArgumentException(
                    $"Invalid {fileType.ToLowerInvariant()} file type. " +
                    $"Allowed types: {string.Join(", ", allowedExtensions)}");
            }

            // =====================================================
            // 4. Validate file size
            // =====================================================

            if (file.Length > maxSize)
            {
                throw new ArgumentException(
                    $"{fileType} size cannot exceed " +
                    $"{maxSize / (1024 * 1024)} MB.");
            }
        }
    }
}