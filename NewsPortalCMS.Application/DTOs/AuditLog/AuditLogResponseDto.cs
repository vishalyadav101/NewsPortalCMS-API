namespace NewsPortalCMS.Application.DTOs.AuditLog
{
    public class AuditLogResponseDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Action { get; set; } = string.Empty;

        public string Module { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string IpAddress { get; set; } = string.Empty;

        public string Browser { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}