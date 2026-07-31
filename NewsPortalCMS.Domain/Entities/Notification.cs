namespace NewsPortalCMS.Domain.Entities;

public class Notification
{
    public int Id { get; set; }


    // User who receives notification

    public string UserId { get; set; } = string.Empty;


    // Notification content

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;


    // Module reference

    public string? Module { get; set; }

    public string? EntityId { get; set; }


    // Status

    public bool IsRead { get; set; } = false;


    // Notification type

    public string Type { get; set; } = string.Empty;


    public DateTime CreatedDate { get; set; }
        = DateTime.UtcNow;


    public DateTime? ReadDate { get; set; }
}