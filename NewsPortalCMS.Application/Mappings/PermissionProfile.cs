using AutoMapper;
using NewsPortalCMS.Application.DTOs.Permission;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Mappings;

public class PermissionProfile : Profile
{
    public PermissionProfile()
    {
        CreateMap<Permission, PermissionResponseDto>();

        CreateMap<CreatePermissionDto, Permission>();

        CreateMap<UpdatePermissionDto, Permission>()
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.RolePermissions, opt => opt.Ignore());
    }
}