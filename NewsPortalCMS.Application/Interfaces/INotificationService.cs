using NewsPortalCMS.Application.DTOs.Notification;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task<NotificationResponseDto> CreateAsync(CreateNotificationDto dto);

        Task<IEnumerable<NotificationResponseDto>> GetAllAsync();

        Task<NotificationResponseDto?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(int id, UpdateNotificationDto dto);

        Task<bool> DeleteAsync(int id);
    }
}