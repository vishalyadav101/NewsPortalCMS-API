using NewsPortalCMS.Application.DTOs.Dashboard;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardDto> GetDashboardStatisticsAsync()
        {
            return await _dashboardRepository.GetDashboardStatisticsAsync();
        }
    }
}