using NewsPortalCMS.Application.DTOs.Public;

namespace NewsPortalCMS.Application.Interfaces.Services;

public interface IPublicStaticPageService
{
    Task<IEnumerable<PublicStaticPageDto>> GetActivePagesAsync();

    Task<PublicStaticPageDto?> GetPageBySlugAsync(string slug);
}