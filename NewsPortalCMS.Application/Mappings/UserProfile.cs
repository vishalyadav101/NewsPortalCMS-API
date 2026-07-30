using AutoMapper;
using NewsPortalCMS.Application.DTOs.User;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserResponseDto>();

        CreateMap<UpdateUserDto, ApplicationUser>();
    }
}