using NewsPortalCMS.Application.DTOs.Dashboard;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardStatisticsAsync();
    }
}