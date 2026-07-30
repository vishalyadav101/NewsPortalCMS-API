namespace NewsPortalCMS.Application.DTOs.Comment;

public class CommentResponseDto
{
    public Guid Id { get; set; }

    public int NewsId { get; set; }

    public string? UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public bool IsApproved { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }
}