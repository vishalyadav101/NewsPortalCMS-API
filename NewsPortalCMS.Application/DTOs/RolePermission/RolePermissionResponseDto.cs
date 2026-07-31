namespace NewsPortalCMS.Application.DTOs.RolePermission;

public class RolePermissionResponseDto
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = string.Empty;

    public List<string> Permissions { get; set; } = new();
}