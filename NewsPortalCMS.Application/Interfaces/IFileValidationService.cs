using Microsoft.AspNetCore.Http;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface IFileValidationService
    {
        void ValidateImage(IFormFile file);

        void ValidateVideo(IFormFile file);

        void ValidateDocument(IFormFile file);
    }
}