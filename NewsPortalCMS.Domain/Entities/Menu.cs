namespace NewsPortalCMS.Domain.Entities;

public class Menu
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedDate { get; set; }

    // Navigation Property
    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}