namespace NewsPortalCMS.Domain.Entities;

public class MenuItem
{
    public int Id { get; set; }

    public int MenuId { get; set; }

    public int? ParentId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public string? Icon { get; set; }

    // _self or _blank
    public string Target { get; set; } = "_self";

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedDate { get; set; }

    // Navigation Properties
    public Menu Menu { get; set; } = null!;

    public MenuItem? Parent { get; set; }

    public ICollection<MenuItem> Children { get; set; } = new List<MenuItem>();
}