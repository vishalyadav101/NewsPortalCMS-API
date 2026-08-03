using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        /// <summary>
        /// Dashboard Summary Report
        /// </summary>
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardReport()
        {
            var result = await _reportService.GetDashboardReportAsync();
            return Ok(result);
        }

        /// <summary>
        /// News Report
        /// </summary>
        [HttpGet("news")]
        public async Task<IActionResult> GetNewsReport()
        {
            var result = await _reportService.GetNewsReportAsync();
            return Ok(result);
        }

        /// <summary>
        /// Comment Report
        /// </summary>
        [HttpGet("comments")]
        public async Task<IActionResult> GetCommentReport()
        {
            var result = await _reportService.GetCommentReportAsync();
            return Ok(result);
        }

        /// <summary>
        /// User Activity Report
        /// </summary>
        [HttpGet("user-activity")]
        public async Task<IActionResult> GetUserActivityReport()
        {
            var result = await _reportService.GetUserActivityReportAsync();
            return Ok(result);
        }
    }
}