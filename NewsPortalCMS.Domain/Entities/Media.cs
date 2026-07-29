using System.ComponentModel.DataAnnotations;

namespace NewsPortalCMS.Domain.Entities;

public class Media
{
    public int Id { get; set; }

    [MaxLength(255)]
    public string FileName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string OriginalFileName { get; set; } = string.Empty;

    [MaxLength(500)]
    public string FilePath { get; set; } = string.Empty;

    [MaxLength(50)]
    public string FileType { get; set; } = string.Empty;

    [MaxLength(100)]
    public string ContentType { get; set; } = string.Empty;

    public long FileSize { get; set; }

    [MaxLength(100)]
    public string UploadedBy { get; set; } = string.Empty;

    public DateTime UploadedDate { get; set; }

    public bool IsActive { get; set; }
}