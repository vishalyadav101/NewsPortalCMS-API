using AutoMapper;
using NewsPortalCMS.Application.DTOs.AuditLog;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Mappings
{
    public class AuditLogProfile : Profile
    {
        public AuditLogProfile()
        {
            CreateMap<CreateAuditLogDto, AuditLog>();

            CreateMap<UpdateAuditLogDto, AuditLog>();

            CreateMap<AuditLog, AuditLogResponseDto>();

            CreateMap<AuditLog, UpdateAuditLogDto>();

            CreateMap<AuditLogResponseDto, AuditLog>();
        }
    }
}