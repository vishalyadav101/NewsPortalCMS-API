using NewsPortalCMS.Application.DTOs.Permission;

namespace NewsPortalCMS.Application.Interfaces.Services;

public interface IPermissionService
{
    Task<IEnumerable<PermissionResponseDto>> GetAllAsync();

    Task<PermissionResponseDto?> GetByIdAsync(Guid id);

    Task<PermissionResponseDto> CreateAsync(CreatePermissionDto dto);

    Task<PermissionResponseDto> UpdateAsync(UpdatePermissionDto dto);

    Task<bool> DeleteAsync(Guid id);
}