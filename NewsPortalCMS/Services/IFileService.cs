using Microsoft.AspNetCore.Http;

namespace NewsPortalCMS.Services.Interfaces
{
    public interface IFileService
    {
        Task<string?> UploadNewsImageAsync(IFormFile? file);
        
    }
}