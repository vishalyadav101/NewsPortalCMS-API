namespace NewsPortalCMS.Application.DTOs.RolePermission;

public class AssignPermissionToRoleDto
{
    public int RoleId { get; set; }

    public List<Guid> PermissionIds { get; set; } = new();
}