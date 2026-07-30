namespace NewsPortalCMS.Application.DTOs.Comment;

public class UpdateCommentDto
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public bool IsApproved { get; set; }

    public bool IsActive { get; set; }
}