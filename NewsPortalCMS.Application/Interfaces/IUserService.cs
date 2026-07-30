using NewsPortalCMS.Application.DTOs.User;

namespace NewsPortalCMS.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();

    Task<UserResponseDto?> GetUserByIdAsync(int id);

    Task<bool> UpdateUserAsync(int id, UpdateUserDto dto);

    Task<bool> UpdateUserStatusAsync(int id, UpdateUserStatusDto dto);

    Task<bool> DeleteUserAsync(int id);
}