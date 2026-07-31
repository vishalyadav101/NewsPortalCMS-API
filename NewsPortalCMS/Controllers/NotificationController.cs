using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.Notification;
using NewsPortalCMS.Application.Interfaces.Services;
namespace NewsPortalCMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNotificationDto dto)
        {
            dto.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? User.FindFirstValue(ClaimTypes.Name)
                         ?? string.Empty;

            var result = await _notificationService.CreateAsync(dto);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _notificationService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _notificationService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateNotificationDto dto)
        {
            var result = await _notificationService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return Ok(new
            {
                Message = "Notification updated successfully."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _notificationService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok(new
            {
                Message = "Notification deleted successfully."
            });
        }
    }
}