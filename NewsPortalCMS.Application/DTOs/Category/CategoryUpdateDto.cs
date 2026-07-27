using System.ComponentModel.DataAnnotations;

namespace NewsPortalCMS.Application.DTOs.Category;

public class CategoryUpdateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string Slug { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public int DisplayOrder { get; set; }
}