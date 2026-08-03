namespace NewsPortalCMS.Application.DTOs.Reports
{
    public class NewsReportDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public string AuthorName { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; }

        public bool IsPublished { get; set; }
    }
}