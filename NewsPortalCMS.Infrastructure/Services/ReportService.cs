using NewsPortalCMS.Application.DTOs.Reports;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<DashboardReportDto> GetDashboardReportAsync()
        {
            return await _reportRepository.GetDashboardReportAsync();
        }

        public async Task<IEnumerable<NewsReportDto>> GetNewsReportAsync()
        {
            return await _reportRepository.GetNewsReportAsync();
        }

        public async Task<IEnumerable<CommentReportDto>> GetCommentReportAsync()
        {
            return await _reportRepository.GetCommentReportAsync();
        }

        public async Task<IEnumerable<UserActivityReportDto>> GetUserActivityReportAsync()
        {
            return await _reportRepository.GetUserActivityReportAsync();
        }
    }
}