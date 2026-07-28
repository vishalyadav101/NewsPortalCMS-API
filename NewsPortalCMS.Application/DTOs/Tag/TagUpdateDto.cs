using System.ComponentModel.DataAnnotations;

namespace NewsPortalCMS.Application.DTOs.Tag;

public class TagUpdateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Slug { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}