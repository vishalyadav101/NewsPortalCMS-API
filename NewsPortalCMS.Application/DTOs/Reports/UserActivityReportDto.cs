namespace NewsPortalCMS.Application.DTOs.Reports
{
    public class UserActivityReportDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public int NewsCreated { get; set; }

        public int CommentsPosted { get; set; }

        public int AuditLogsGenerated { get; set; }
    }
}