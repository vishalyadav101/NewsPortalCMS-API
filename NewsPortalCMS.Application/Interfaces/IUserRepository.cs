using Microsoft.AspNetCore.Identity;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<ApplicationUser>> GetAllAsync();

    Task<ApplicationUser?> GetByIdAsync(int id);

    Task<IdentityResult> UpdateAsync(ApplicationUser user);

    Task<IdentityResult> DeleteAsync(ApplicationUser user);
}