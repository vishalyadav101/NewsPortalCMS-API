using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateAsync(Notification notification);

        Task<Notification?> GetByIdAsync(int id);

        Task<IEnumerable<Notification>> GetAllAsync();

        Task UpdateAsync(Notification notification);

        Task DeleteAsync(Notification notification);
    }
}