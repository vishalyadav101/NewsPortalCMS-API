using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories;

public interface IRolePermissionRepository
{
    Task<IEnumerable<RolePermission>> GetByRoleIdAsync(int roleId);

    Task AddRangeAsync(IEnumerable<RolePermission> rolePermissions);

    Task RemoveRangeAsync(IEnumerable<RolePermission> rolePermissions);
}