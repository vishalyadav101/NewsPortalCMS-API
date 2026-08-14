using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NewsPortalCMS.Application.DTOs.Profile;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services;

public class ProfileService : IProfileService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IFileStorageService _fileStorageService;
    private readonly IFileValidationService _fileValidationService;

    public ProfileService(
        UserManager<ApplicationUser> userManager,
        IFileStorageService fileStorageService,
        IFileValidationService fileValidationService)
    {
        _userManager = userManager;
        _fileStorageService = fileStorageService;
        _fileValidationService = fileValidationService;
    }

    // ============================================
    // GET MY PROFILE
    // ============================================

    public async Task<ProfileResponseDto?> GetMyProfileAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return null;

        return new ProfileResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName ?? string.Empty,
            Email = user.Email ?? string.Empty,
            ProfileImage = user.ProfileImage,
            IsActive = user.IsActive,
            CreatedDate = user.CreatedDate
        };
    }

    // ============================================
    // UPDATE MY PROFILE
    // ============================================

    public async Task<bool> UpdateMyProfileAsync(
     int userId,
     UpdateProfileDto dto)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return false;

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.UpdatedDate = DateTime.UtcNow;

        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded;
    }
    // ============================================
    // UPLOAD / CHANGE PROFILE IMAGE
    // ============================================

    public async Task<string?> UploadProfileImageAsync(
        int userId,
        IFormFile file)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return null;

        // Validate image
        _fileValidationService.ValidateImage(file);

        // Save new image
        var newImageUrl =
            await _fileStorageService.SaveAsync(
                file,
                "profiles");

        if (string.IsNullOrWhiteSpace(newImageUrl))
            return null;

        // Delete old image after new image is successfully saved
        if (!string.IsNullOrWhiteSpace(user.ProfileImage))
        {
            await _fileStorageService.DeleteAsync(
                user.ProfileImage);
        }

        user.ProfileImage = newImageUrl;
        user.UpdatedDate = DateTime.UtcNow;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            // If database update fails, remove the newly uploaded image
            await _fileStorageService.DeleteAsync(newImageUrl);

            return null;
        }

        return newImageUrl;
    }

    // ============================================
    // DELETE PROFILE IMAGE
    // ============================================

    public async Task<bool> DeleteProfileImageAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            return false;

        if (string.IsNullOrWhiteSpace(user.ProfileImage))
            return true;

        var oldImageUrl = user.ProfileImage;

        user.ProfileImage = null;
        user.UpdatedDate = DateTime.UtcNow;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return false;

        // Delete physical image
        await _fileStorageService.DeleteAsync(oldImageUrl);

        return true;
    }
}