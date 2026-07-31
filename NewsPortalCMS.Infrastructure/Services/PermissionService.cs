using AutoMapper;
using NewsPortalCMS.Application.DTOs.Permission;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    public PermissionService(
        IPermissionRepository permissionRepository,
        IMapper mapper)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PermissionResponseDto>> GetAllAsync()
    {
        var permissions = await _permissionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PermissionResponseDto>>(permissions);
    }

    public async Task<PermissionResponseDto?> GetByIdAsync(Guid id)
    {
        var permission = await _permissionRepository.GetByIdAsync(id);

        if (permission == null)
            return null;

        return _mapper.Map<PermissionResponseDto>(permission);
    }

    public async Task<PermissionResponseDto> CreateAsync(CreatePermissionDto dto)
    {
        var existingPermission = await _permissionRepository.GetByCodeAsync(dto.Code);

        if (existingPermission != null)
            throw new Exception("Permission code already exists.");

        var permission = _mapper.Map<Permission>(dto);

        permission.Id = Guid.NewGuid();
        permission.CreatedDate = DateTime.UtcNow;

        await _permissionRepository.AddAsync(permission);

        return _mapper.Map<PermissionResponseDto>(permission);
    }

    public async Task<PermissionResponseDto> UpdateAsync(UpdatePermissionDto dto)
    {
        var permission = await _permissionRepository.GetByIdAsync(dto.Id);

        if (permission == null)
            throw new Exception("Permission not found.");

        var existingPermission = await _permissionRepository.GetByCodeAsync(dto.Code);

        if (existingPermission != null && existingPermission.Id != dto.Id)
            throw new Exception("Permission code already exists.");

        _mapper.Map(dto, permission);

        permission.UpdatedDate = DateTime.UtcNow;

        await _permissionRepository.UpdateAsync(permission);

        return _mapper.Map<PermissionResponseDto>(permission);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var permission = await _permissionRepository.GetByIdAsync(id);

        if (permission == null)
            return false;

        await _permissionRepository.DeleteAsync(permission);

        return true;
    }
}