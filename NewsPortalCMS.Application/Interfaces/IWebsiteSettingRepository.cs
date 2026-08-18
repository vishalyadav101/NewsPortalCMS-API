using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories;

public interface IWebsiteSettingRepository
{
    Task<WebsiteSetting?> GetAsync();

    Task<WebsiteSetting?> GetByIdAsync(int id);

    Task<WebsiteSetting> AddAsync(WebsiteSetting entity);

    Task UpdateAsync(WebsiteSetting entity);

    Task DeleteAsync(WebsiteSetting entity);
}