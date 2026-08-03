namespace NewsPortalCMS.Application.DTOs.Public
{
    public class PublicMenuItemDto
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public string? Icon { get; set; }

        public string Target { get; set; } = "_self";

        public int DisplayOrder { get; set; }

        public List<PublicMenuItemDto> Children { get; set; } = new();
    }
}