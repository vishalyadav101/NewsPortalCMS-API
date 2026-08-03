namespace NewsPortalCMS.Application.DTOs.Reports
{
    public class CommentReportDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string NewsTitle { get; set; } = string.Empty;

        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}