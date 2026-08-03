using NewsPortalCMS.Application.DTOs.Public;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface IPublicMenuService
    {
        Task<IEnumerable<PublicMenuDto>> GetActiveMenusAsync();

        Task<PublicMenuDto?> GetMenuByLocationAsync(string location);
    }
}