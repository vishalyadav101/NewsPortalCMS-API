namespace NewsPortalCMS.Application.DTOs.Public
{
    public class PublicCommentDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}