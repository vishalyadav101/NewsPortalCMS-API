namespace NewsPortalCMS.Application.DTOs.Profile;

public class ProfileResponseDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? ProfileImage { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }
}