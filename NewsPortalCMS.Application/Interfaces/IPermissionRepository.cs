using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories;

public interface IPermissionRepository
{
    Task<IEnumerable<Permission>> GetAllAsync();

    Task<Permission?> GetByIdAsync(Guid id);

    Task<Permission?> GetByCodeAsync(string code);

    Task AddAsync(Permission permission);

    Task UpdateAsync(Permission permission);

    Task DeleteAsync(Permission permission);
}