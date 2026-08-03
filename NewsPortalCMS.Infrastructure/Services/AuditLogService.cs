using AutoMapper;
using NewsPortalCMS.Application.DTOs.AuditLog;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Infrastructure.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IMapper _mapper;

        public AuditLogService(
            IAuditLogRepository auditLogRepository,
            IMapper mapper)
        {
            _auditLogRepository = auditLogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuditLogResponseDto>> GetAllAsync()
        {
            var auditLogs = await _auditLogRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<AuditLogResponseDto>>(auditLogs);
        }

        public async Task<AuditLogResponseDto?> GetByIdAsync(Guid id)
        {
            var auditLog = await _auditLogRepository.GetByIdAsync(id);

            if (auditLog == null)
                return null;

            return _mapper.Map<AuditLogResponseDto>(auditLog);
        }

        public async Task<AuditLogResponseDto> CreateAsync(CreateAuditLogDto dto)
        {
            var auditLog = _mapper.Map<AuditLog>(dto);

            auditLog.Id = Guid.NewGuid();
            auditLog.CreatedAt = DateTime.UtcNow;

            await _auditLogRepository.AddAsync(auditLog);

            return _mapper.Map<AuditLogResponseDto>(auditLog);
        }

        public async Task<bool> UpdateAsync(UpdateAuditLogDto dto)
        {
            var auditLog = await _auditLogRepository.GetByIdAsync(dto.Id);

            if (auditLog == null)
                return false;

            _mapper.Map(dto, auditLog);

            await _auditLogRepository.UpdateAsync(auditLog);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var auditLog = await _auditLogRepository.GetByIdAsync(id);

            if (auditLog == null)
                return false;

            await _auditLogRepository.DeleteAsync(auditLog);

            return true;
        }
    }
}