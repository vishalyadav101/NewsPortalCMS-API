namespace NewsPortalCMS.Application.DTOs.Notification
{
    public class UpdateNotificationDto
    {
        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public bool IsRead { get; set; }

        public string? Module { get; set; }

        public string? EntityId { get; set; }

        public DateTime? ReadDate { get; set; }
    }
}