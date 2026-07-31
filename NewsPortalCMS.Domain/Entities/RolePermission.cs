using Microsoft.AspNetCore.Identity;

namespace NewsPortalCMS.Domain.Entities;

public class RolePermission
{
    public Guid Id { get; set; }

    public int RoleId { get; set; }

    public Guid PermissionId { get; set; }

    public IdentityRole<int> Role { get; set; } = null!;

    public Permission Permission { get; set; } = null!;
}