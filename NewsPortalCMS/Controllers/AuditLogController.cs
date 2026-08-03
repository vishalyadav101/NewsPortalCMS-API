using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.AuditLog;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var auditLogs = await _auditLogService.GetAllAsync();
            return Ok(auditLogs);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var auditLog = await _auditLogService.GetByIdAsync(id);

            if (auditLog == null)
                return NotFound();

            return Ok(auditLog);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuditLogDto dto)
        {
            var createdAuditLog = await _auditLogService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdAuditLog.Id },
                createdAuditLog);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateAuditLogDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Route ID and DTO ID do not match.");

            var updated = await _auditLogService.UpdateAsync(dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _auditLogService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}