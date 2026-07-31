using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Replace with your existing role/permission policy if required
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardStatistics()
        {
            var result = await _dashboardService.GetDashboardStatisticsAsync();

            return Ok(result);
        }
    }
}