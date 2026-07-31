namespace NewsPortalCMS.Application.DTOs.Notification
{
    public class NotificationResponseDto
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string? Module { get; set; }

        public string? EntityId { get; set; }

        public bool IsRead { get; set; }

        public string Type { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public DateTime? ReadDate { get; set; }
    }
}