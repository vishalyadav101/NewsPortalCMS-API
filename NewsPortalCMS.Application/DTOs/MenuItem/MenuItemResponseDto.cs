namespace NewsPortalCMS.Application.DTOs.MenuItem;

public class MenuItemResponseDto
{
    public int Id { get; set; }

    public int MenuId { get; set; }

    public int? ParentId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public string? Icon { get; set; }

    public string Target { get; set; } = "_self";

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}