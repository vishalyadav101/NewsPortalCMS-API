using AutoMapper;
using NewsPortalCMS.Application.DTOs.Notification;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Mappings
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<CreateNotificationDto, Notification>();

            CreateMap<UpdateNotificationDto, Notification>();

            CreateMap<Notification, NotificationResponseDto>();
        }
    }
}