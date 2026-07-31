using NewsPortalCMS.Application.DTOs.RolePermission;

namespace NewsPortalCMS.Application.Interfaces.Services;

public interface IRolePermissionService
{
    Task AssignPermissionsAsync(AssignPermissionToRoleDto dto);

    Task<RolePermissionResponseDto> GetRolePermissionsAsync(int roleId);
}