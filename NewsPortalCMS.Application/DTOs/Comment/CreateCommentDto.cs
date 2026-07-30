namespace NewsPortalCMS.Application.DTOs.Comment;

public class CreateCommentDto
{
    public int NewsId { get; set; }

    public string? UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
}