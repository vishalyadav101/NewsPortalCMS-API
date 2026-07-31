using Microsoft.AspNetCore.Identity;
using NewsPortalCMS.Application.DTOs.RolePermission;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services;

public class RolePermissionService : IRolePermissionService
{
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public RolePermissionService(
        IRolePermissionRepository rolePermissionRepository,
        RoleManager<IdentityRole<int>> roleManager)
    {
        _rolePermissionRepository = rolePermissionRepository;
        _roleManager = roleManager;
    }

    public async Task AssignPermissionsAsync(AssignPermissionToRoleDto dto)
    {
        var existing = await _rolePermissionRepository.GetByRoleIdAsync(dto.RoleId);

        if (existing.Any())
        {
            await _rolePermissionRepository.RemoveRangeAsync(existing);
        }

        var rolePermissions = dto.PermissionIds.Select(permissionId =>
            new RolePermission
            {
                Id = Guid.NewGuid(),
                RoleId = dto.RoleId,
                PermissionId = permissionId
            });

        await _rolePermissionRepository.AddRangeAsync(rolePermissions);
    }

    public async Task<RolePermissionResponseDto> GetRolePermissionsAsync(int roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());

        if (role == null)
            throw new Exception("Role not found.");

        var rolePermissions = await _rolePermissionRepository.GetByRoleIdAsync(roleId);

        return new RolePermissionResponseDto
        {
            RoleId = roleId,
            RoleName = role.Name!,
            Permissions = rolePermissions
                .Select(x => x.Permission.Name)
                .ToList()
        };
    }
}