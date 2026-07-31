namespace NewsPortalCMS.Application.DTOs.Permission;

public class CreatePermissionDto
{
    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Module { get; set; } = string.Empty;
}