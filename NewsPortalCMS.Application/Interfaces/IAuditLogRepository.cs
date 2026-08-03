using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories
{
    public interface IAuditLogRepository
    {
        Task<IEnumerable<AuditLog>> GetAllAsync();

        Task<AuditLog?> GetByIdAsync(Guid id);

        Task AddAsync(AuditLog auditLog);

        Task UpdateAsync(AuditLog auditLog);

        Task DeleteAsync(AuditLog auditLog);
    }
}