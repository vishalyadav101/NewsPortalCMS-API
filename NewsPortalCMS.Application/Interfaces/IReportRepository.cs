using NewsPortalCMS.Application.DTOs.Reports;

namespace NewsPortalCMS.Application.Interfaces.Repositories
{
    public interface IReportRepository
    {
        Task<DashboardReportDto> GetDashboardReportAsync();

        Task<IEnumerable<NewsReportDto>> GetNewsReportAsync();

        Task<IEnumerable<CommentReportDto>> GetCommentReportAsync();

        Task<IEnumerable<UserActivityReportDto>> GetUserActivityReportAsync();
    }
}