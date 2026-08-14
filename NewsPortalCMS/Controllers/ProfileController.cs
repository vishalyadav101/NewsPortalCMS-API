using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.Profile;
using NewsPortalCMS.Application.Interfaces;

namespace NewsPortalCMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    // ============================================
    // GET MY PROFILE
    // ============================================

    [HttpGet]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = GetUserId();

        if (userId == null)
            return Unauthorized(new
            {
                Message = "Invalid user identity."
            });

        var profile =
            await _profileService.GetMyProfileAsync(userId.Value);

        if (profile == null)
            return NotFound(new
            {
                Message = "User profile not found."
            });

        return Ok(profile);
    }

    // ============================================
    // UPDATE MY PROFILE
    // ============================================

    [HttpPut]
    public async Task<IActionResult> UpdateMyProfile(
     UpdateProfileDto dto)
    {
        var userId = GetUserId();

        if (userId == null)
            return Unauthorized(new
            {
                Message = "Invalid user identity."
            });

        var result =
            await _profileService.UpdateMyProfileAsync(
                userId.Value,
                dto);

        if (!result)
            return NotFound(new
            {
                Message = "User profile not found."
            });

        return Ok(new
        {
            Message = "Profile updated successfully."
        });
    }
    // ============================================
    // UPLOAD / CHANGE PROFILE IMAGE
    // ============================================

    [HttpPost("image")]
    public async Task<IActionResult> UploadProfileImage(
        IFormFile file)
    {
        var userId = GetUserId();

        if (userId == null)
            return Unauthorized(new
            {
                Message = "Invalid user identity."
            });

        try
        {
            var imageUrl =
                await _profileService.UploadProfileImageAsync(
                    userId.Value,
                    file);

            if (imageUrl == null)
                return NotFound(new
                {
                    Message = "User profile not found."
                });

            return Ok(new
            {
                Message = "Profile image uploaded successfully.",
                ProfileImage = imageUrl
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                Message = ex.Message
            });
        }
    }

    // ============================================
    // DELETE PROFILE IMAGE
    // ============================================

    [HttpDelete("image")]
    public async Task<IActionResult> DeleteProfileImage()
    {
        var userId = GetUserId();

        if (userId == null)
            return Unauthorized(new
            {
                Message = "Invalid user identity."
            });

        var result =
            await _profileService.DeleteProfileImageAsync(
                userId.Value);

        if (!result)
            return NotFound(new
            {
                Message = "User profile not found."
            });

        return Ok(new
        {
            Message = "Profile image removed successfully."
        });
    }

    // ============================================
    // GET LOGGED-IN USER ID FROM JWT
    // ============================================

    private int? GetUserId()
    {
        var userIdClaim =
            User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userIdClaim))
            return null;

        if (!int.TryParse(userIdClaim, out var userId))
            return null;

        return userId;
    }
}