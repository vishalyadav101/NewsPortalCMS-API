using AutoMapper;
using NewsPortalCMS.Application.DTOs.Notification;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;
namespace NewsPortalCMS.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;


        public NotificationService(
            INotificationRepository notificationRepository,
            IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }


        public async Task<NotificationResponseDto> CreateAsync(CreateNotificationDto dto)
        {
            var notification = _mapper.Map<Notification>(dto);

            var result = await _notificationRepository
                .CreateAsync(notification);

            return _mapper.Map<NotificationResponseDto>(result);
        }


        public async Task<IEnumerable<NotificationResponseDto>> GetAllAsync()
        {
            var notifications = await _notificationRepository
                .GetAllAsync();

            return _mapper.Map<IEnumerable<NotificationResponseDto>>(notifications);
        }


        public async Task<NotificationResponseDto?> GetByIdAsync(int id)
        {
            var notification = await _notificationRepository
                .GetByIdAsync(id);

            if (notification == null)
                return null;

            return _mapper.Map<NotificationResponseDto>(notification);
        }


        public async Task<bool> UpdateAsync(
            int id,
            UpdateNotificationDto dto)
        {
            var notification = await _notificationRepository
                .GetByIdAsync(id);

            if (notification == null)
                return false;


            _mapper.Map(dto, notification);


            await _notificationRepository
                .UpdateAsync(notification);


            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var notification = await _notificationRepository
                .GetByIdAsync(id);

            if (notification == null)
                return false;


            await _notificationRepository
                .DeleteAsync(notification);


            return true;
        }
    }
}