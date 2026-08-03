using NewsPortalCMS.Application.DTOs.Public.Seo;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface IPublicSeoService
    {
        Task<PublicSeoDto?> GetSeoByPageNameAsync(string pageName);
    }
}