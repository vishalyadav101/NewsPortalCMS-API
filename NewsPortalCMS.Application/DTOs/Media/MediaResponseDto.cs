namespace NewsPortalCMS.Application.DTOs.Media;

public class MediaResponseDto
{
    public int Id { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string OriginalFileName { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public string FileType { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public long FileSize { get; set; }

    public string UploadedBy { get; set; } = string.Empty;

    public DateTime UploadedDate { get; set; }

    public bool IsActive { get; set; }
}