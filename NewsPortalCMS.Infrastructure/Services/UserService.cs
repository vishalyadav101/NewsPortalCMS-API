using AutoMapper;
using NewsPortalCMS.Application.DTOs.User;
using NewsPortalCMS.Application.Interfaces;

namespace NewsPortalCMS.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<UserResponseDto>>(users);
    }

    public async Task<UserResponseDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            return null;

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<bool> UpdateUserAsync(int id, UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            return false;

        _mapper.Map(dto, user);

        user.UpdatedDate = DateTime.UtcNow;

        var result = await _userRepository.UpdateAsync(user);

        return result.Succeeded;
    }

    public async Task<bool> UpdateUserStatusAsync(int id, UpdateUserStatusDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            return false;

        user.IsActive = dto.IsActive;
        user.UpdatedDate = DateTime.UtcNow;

        var result = await _userRepository.UpdateAsync(user);

        return result.Succeeded;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
            return false;

        var result = await _userRepository.DeleteAsync(user);

        return result.Succeeded;
    }
}