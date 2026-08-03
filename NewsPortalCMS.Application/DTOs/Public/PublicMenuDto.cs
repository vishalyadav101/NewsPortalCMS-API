namespace NewsPortalCMS.Application.DTOs.Public
{
    public class PublicMenuDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public List<PublicMenuItemDto> MenuItems { get; set; } = new();
    }
}