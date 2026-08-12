using Microsoft.AspNetCore.Http;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface IFileStorageService
    {
        Task<string?> SaveAsync(
            IFormFile file,
            string folder,
            CancellationToken cancellationToken = default);

        Task<string?> SaveImageWithThumbnailAsync(
            IFormFile file,
            string folder,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(
            string fileUrl,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(
            string fileUrl,
            CancellationToken cancellationToken = default);
        Task<bool> DeleteWithThumbnailAsync(
    string fileUrl,
    CancellationToken cancellationToken = default);
    }
}