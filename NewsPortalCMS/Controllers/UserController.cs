using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.User;
using NewsPortalCMS.Application.Interfaces;

namespace NewsPortalCMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
            return NotFound(new { Message = "User not found." });

        return Ok(user);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto dto)
    {
        var result = await _userService.UpdateUserAsync(id, dto);

        if (!result)
            return NotFound(new { Message = "User not found." });

        return Ok(new { Message = "User updated successfully." });
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> UpdateUserStatus(int id, UpdateUserStatusDto dto)
    {
        var result = await _userService.UpdateUserStatusAsync(id, dto);

        if (!result)
            return NotFound(new { Message = "User not found." });

        return Ok(new { Message = "User status updated successfully." });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var result = await _userService.DeleteUserAsync(id);

        if (!result)
            return NotFound(new { Message = "User not found." });

        return Ok(new { Message = "User deleted successfully." });
    }
}