using NewsPortalCMS.Application.DTOs.Role;

namespace NewsPortalCMS.Application.Interfaces.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleResponseDto>> GetAllAsync();

    Task<RoleResponseDto?> GetByIdAsync(int id);

    Task<RoleResponseDto> CreateAsync(CreateRoleDto dto);

    Task<RoleResponseDto> UpdateAsync(UpdateRoleDto dto);

    Task<bool> DeleteAsync(int id);
}