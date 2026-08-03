using NewsPortalCMS.Application.DTOs.Reports;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface IReportService
    {
        Task<DashboardReportDto> GetDashboardReportAsync();

        Task<IEnumerable<NewsReportDto>> GetNewsReportAsync();

        Task<IEnumerable<CommentReportDto>> GetCommentReportAsync();

        Task<IEnumerable<UserActivityReportDto>> GetUserActivityReportAsync();
    }
}