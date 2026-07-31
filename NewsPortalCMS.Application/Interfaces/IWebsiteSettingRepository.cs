using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories;

public interface IWebsiteSettingRepository
{
    Task<WebsiteSetting?> GetAsync();

    Task<WebsiteSetting?> GetByIdAsync(int id);

    Task<WebsiteSetting> AddAsync(
        WebsiteSetting websiteSetting);

    Task UpdateAsync(
        WebsiteSetting websiteSetting);

    Task<bool> DeleteAsync(int id);
}