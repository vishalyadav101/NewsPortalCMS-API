using Microsoft.AspNetCore.Identity;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Infrastructure.Seed;

public static class IdentitySeeder
{
    public static async Task SeedRolesAsync(
        RoleManager<IdentityRole<int>> roleManager)
    {
        string[] roles =
        {
            "SuperAdmin",
            "Admin",
            "Editor",
            "Reporter"
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(
                    new IdentityRole<int>(role));
            }
        }
    }

    public static async Task AssignSuperAdminAsync(
        UserManager<ApplicationUser> userManager)
    {
        var user = await userManager.FindByNameAsync("vivek");

        if (user == null)
        {
            return;
        }

        if (!await userManager.IsInRoleAsync(user, "SuperAdmin"))
        {
            await userManager.AddToRoleAsync(user, "SuperAdmin");
        }
    }
}