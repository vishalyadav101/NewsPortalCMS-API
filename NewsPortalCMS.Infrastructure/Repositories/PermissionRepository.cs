using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly ApplicationDbContext _context;

    public PermissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        return await _context.Permissions
            .OrderBy(p => p.Module)
            .ThenBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Permission?> GetByIdAsync(Guid id)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Permission?> GetByCodeAsync(string code)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task AddAsync(Permission permission)
    {
        await _context.Permissions.AddAsync(permission);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Permission permission)
    {
        _context.Permissions.Update(permission);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Permission permission)
    {
        _context.Permissions.Remove(permission);
        await _context.SaveChangesAsync();
    }
}