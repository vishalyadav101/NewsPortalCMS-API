namespace NewsPortalCMS.Application.DTOs.Reports
{
    public class DashboardReportDto
    {
        public int TotalNews { get; set; }

        public int TotalCategories { get; set; }

        public int TotalSubCategories { get; set; }

        public int TotalTags { get; set; }

        public int TotalUsers { get; set; }

        public int TotalComments { get; set; }

        public int TotalAdvertisements { get; set; }

        public int TotalNotifications { get; set; }

        public int TotalStaticPages { get; set; }

        public int TotalMenus { get; set; }

        public int TotalAuditLogs { get; set; }
    }
}