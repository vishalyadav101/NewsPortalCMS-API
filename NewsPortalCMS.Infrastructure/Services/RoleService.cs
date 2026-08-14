using Microsoft.AspNetCore.Identity;
using NewsPortalCMS.Application.DTOs.Role;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Application.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public RoleService(RoleManager<IdentityRole<int>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<RoleResponseDto>> GetAllAsync()
    {
        var roles = _roleManager.Roles
            .OrderBy(r => r.Name)
            .ToList();

        return roles.Select(role => new RoleResponseDto
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty
        });
    }

    public async Task<RoleResponseDto?> GetByIdAsync(int id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());

        if (role == null)
            return null;

        return new RoleResponseDto
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty
        };
    }

    public async Task<RoleResponseDto> CreateAsync(CreateRoleDto dto)
    {
        var roleName = dto.Name.Trim();

        if (string.IsNullOrWhiteSpace(roleName))
            throw new Exception("Role name is required.");

        var existingRole = await _roleManager.FindByNameAsync(roleName);

        if (existingRole != null)
            throw new Exception("Role already exists.");

        var role = new IdentityRole<int>
        {
            Name = roleName
        };

        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
        {
            var errors = string.Join(
                ", ",
                result.Errors.Select(x => x.Description));

            throw new Exception(errors);
        }

        return new RoleResponseDto
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty
        };
    }

    public async Task<RoleResponseDto> UpdateAsync(UpdateRoleDto dto)
    {
        var role = await _roleManager.FindByIdAsync(dto.Id.ToString());

        if (role == null)
            throw new Exception("Role not found.");

        var roleName = dto.Name.Trim();

        if (string.IsNullOrWhiteSpace(roleName))
            throw new Exception("Role name is required.");

        var existingRole = await _roleManager.FindByNameAsync(roleName);

        if (existingRole != null && existingRole.Id != dto.Id)
            throw new Exception("Role already exists.");

        role.Name = roleName;

        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded)
        {
            var errors = string.Join(
                ", ",
                result.Errors.Select(x => x.Description));

            throw new Exception(errors);
        }

        return new RoleResponseDto
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());

        if (role == null)
            return false;

        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded)
        {
            var errors = string.Join(
                ", ",
                result.Errors.Select(x => x.Description));

            throw new Exception(errors);
        }

        return true;
    }
}