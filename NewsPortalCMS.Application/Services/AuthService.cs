using Microsoft.AspNetCore.Identity;
using NewsPortalCMS.Application.DTOs.Auth;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtTokenService _jwtTokenService;
    private readonly IFileStorageService _fileStorageService;
    private readonly IFileValidationService _fileValidationService;

    public AuthService(
    UserManager<ApplicationUser> userManager,
    JwtTokenService jwtTokenService,
    IFileStorageService fileStorageService,
    IFileValidationService fileValidationService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
        _fileStorageService = fileStorageService;
        _fileValidationService = fileValidationService;
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

        // ============================================
        // Profile Image Upload
        // ============================================

        string? profileImageUrl = null;

        if (model.ProfileImage != null)
        {
            // Validate profile image
            _fileValidationService.ValidateImage(model.ProfileImage);

            // Save image in wwwroot/uploads/profiles
            profileImageUrl = await _fileStorageService.SaveAsync(
                model.ProfileImage,
                "profiles");
        }

        // ============================================
        // Create User
        // ============================================

        var user = new ApplicationUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
            Email = model.Email,
            ProfileImage = profileImageUrl,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(
            user,
            model.Password);

        // ============================================
        // Cleanup uploaded image if user creation fails
        // ============================================

        if (!result.Succeeded)
        {
            if (!string.IsNullOrWhiteSpace(profileImageUrl))
            {
                await _fileStorageService.DeleteAsync(profileImageUrl);
            }

            return string.Join(
                ", ",
                result.Errors.Select(x => x.Description));
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

        if (!user.IsActive)
        {
            return "User account is inactive";
        }

        var isPasswordValid =
            await _userManager.CheckPasswordAsync(user, model.Password);

        if (!isPasswordValid)
        {
            return "Invalid Password";
        }

        var token = await _jwtTokenService.GenerateTokenAsync(user);

        return token;
    }
}