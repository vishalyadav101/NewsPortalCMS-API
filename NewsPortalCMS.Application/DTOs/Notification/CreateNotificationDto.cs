namespace NewsPortalCMS.Application.DTOs.Notification
{
    public class CreateNotificationDto
    {
        public string UserId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string? Module { get; set; }

        public string? EntityId { get; set; }
    }
}