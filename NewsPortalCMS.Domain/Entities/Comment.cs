using NewsPortalCMS.Entities;

namespace NewsPortalCMS.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }

    public int NewsId { get; set; }

    public string? UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public bool IsApproved { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedDate { get; set; }

    // Navigation Properties

    public News News { get; set; } = null!;
}