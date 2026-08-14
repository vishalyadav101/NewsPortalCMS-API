using Microsoft.AspNetCore.Http;
using NewsPortalCMS.Application.DTOs.Profile;

namespace NewsPortalCMS.Application.Interfaces;

public interface IProfileService
{
    Task<ProfileResponseDto?> GetMyProfileAsync(int userId);

    Task<bool> UpdateMyProfileAsync(
        int userId,
        UpdateProfileDto dto);

    Task<string?> UploadProfileImageAsync(
        int userId,
        IFormFile file);

    Task<bool> DeleteProfileImageAsync(int userId);
}