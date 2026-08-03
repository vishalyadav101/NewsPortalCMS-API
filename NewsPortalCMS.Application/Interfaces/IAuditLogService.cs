using NewsPortalCMS.Application.DTOs.AuditLog;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLogResponseDto>> GetAllAsync();

        Task<AuditLogResponseDto?> GetByIdAsync(Guid id);

        Task<AuditLogResponseDto> CreateAsync(CreateAuditLogDto dto);

        Task<bool> UpdateAsync(UpdateAuditLogDto dto);

        Task<bool> DeleteAsync(Guid id);
    }
}