namespace NewsPortalCMS.Application.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int TotalUsers { get; set; }

        public int TotalNews { get; set; }

        public int PublishedNews { get; set; }

        public int DraftNews { get; set; }

        public int TotalCategories { get; set; }

        public int TotalSubCategories { get; set; }

        public int TotalTags { get; set; }

        public int TotalComments { get; set; }

        public int PendingComments { get; set; }

        public int TotalAdvertisements { get; set; }

        public int ActiveAdvertisements { get; set; }

        public int TotalStaticPages { get; set; }
    }
}