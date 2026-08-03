using System.ComponentModel.DataAnnotations;

namespace NewsPortalCMS.Application.DTOs.AuditLog
{
    public class CreateAuditLogDto
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Action { get; set; } = string.Empty;

        [Required]
        public string Module { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string IpAddress { get; set; } = string.Empty;

        public string Browser { get; set; } = string.Empty;
    }
}