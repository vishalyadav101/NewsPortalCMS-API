using NewsPortalCMS.Application.DTOs.Dashboard;

namespace NewsPortalCMS.Application.Interfaces.Repositories
{
    public interface IDashboardRepository
    {
        Task<DashboardDto> GetDashboardStatisticsAsync();
    }
}