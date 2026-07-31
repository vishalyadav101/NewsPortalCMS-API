using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.RolePermission;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RolePermissionController : ControllerBase
{
    private readonly IRolePermissionService _rolePermissionService;

    public RolePermissionController(IRolePermissionService rolePermissionService)
    {
        _rolePermissionService = rolePermissionService;
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignPermissions(AssignPermissionToRoleDto dto)
    {
        await _rolePermissionService.AssignPermissionsAsync(dto);
        return Ok(new
        {
            Message = "Permissions assigned successfully."
        });
    }

    [HttpGet("{roleId}")]
    public async Task<IActionResult> GetRolePermissions(int roleId)
    {
        var result = await _rolePermissionService.GetRolePermissionsAsync(roleId);
        return Ok(result);
    }
}