namespace NewsPortalCMS.Domain.Entities;

public class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedDate { get; set; }
    public ICollection<NewsTag> NewsTags { get; set; } = new List<NewsTag>();
}