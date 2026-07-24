using Microsoft.AspNetCore.Identity;
using NewsPortalCMS.Application.DTOs.Auth;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> RegisterAsync(RegisterDto model)
    {
        // Check username
        var existingUser = await _userManager.FindByNameAsync(model.UserName);

        if (existingUser != null)
        {
            return "Username already exists.";
        }

        // Check email
        var existingEmail = await _userManager.FindByEmailAsync(model.Email);

        if (existingEmail != null)
        {
            return "Email already exists.";
        }

        // Create user
        var user = new ApplicationUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
            Email = model.Email,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return string.Join(", ", result.Errors.Select(x => x.Description));
        }

        return "User Registered Successfully";
    }

    public async Task<string> LoginAsync(LoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user == null)
        {
            return "Invalid Username";
        }

        var result = await _userManager.CheckPasswordAsync(user, model.Password);

        if (!result)
        {
            return "Invalid Password";
        }

        return "Login Successful";
    }
}