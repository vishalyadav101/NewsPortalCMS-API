using System.ComponentModel.DataAnnotations;

namespace NewsPortalCMS.Application.DTOs.Profile;

public class UpdateProfileDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;
}