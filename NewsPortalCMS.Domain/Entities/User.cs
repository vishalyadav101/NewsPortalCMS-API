namespace NewsPortalCMS.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string? ProfileImage { get; set; }

    public bool EmailConfirmed { get; set; } = false;

    public DateTime? LastLogin { get; set; }
}