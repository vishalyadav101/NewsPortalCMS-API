using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.DTOs.Reports;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardReportDto> GetDashboardReportAsync()
        {
            return new DashboardReportDto
            {
                TotalNews = await _context.News.CountAsync(),
                TotalCategories = await _context.Categories.CountAsync(),
                TotalSubCategories = await _context.SubCategories.CountAsync(),
                TotalTags = await _context.Tags.CountAsync(),
                TotalUsers = await _context.Users.CountAsync(),
                TotalComments = await _context.Comments.CountAsync(),
                TotalAdvertisements = await _context.Advertisements.CountAsync(),
                TotalNotifications = await _context.Notifications.CountAsync(),
                TotalStaticPages = await _context.StaticPages.CountAsync(),
                TotalMenus = await _context.Menus.CountAsync(),
                TotalAuditLogs = await _context.AuditLogs.CountAsync()
            };
        }

        public async Task<IEnumerable<NewsReportDto>> GetNewsReportAsync()
        {
            return await _context.News
                .Include(n => n.Category)
                .Select(n => new NewsReportDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    CategoryName = n.Category != null ? n.Category.Name : string.Empty,
                    AuthorName = n.Author ?? string.Empty,
                    PublishedDate = n.PublishDate,
                    IsPublished = n.IsPublished
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<CommentReportDto>> GetCommentReportAsync()
        {
            return await _context.Comments
                .Include(c => c.News)
                .Select(c => new CommentReportDto
                {
                    Id = c.Id,
                    UserName = c.Name,
                    NewsTitle = c.News.Title,
                    Comment = c.Content,
                    CreatedAt = c.CreatedDate
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<UserActivityReportDto>> GetUserActivityReportAsync()
        {
            return await _context.Users
                .Select(u => new UserActivityReportDto
                {
                    UserId = u.Id,
                    UserName = u.FirstName + " " + u.LastName,
                    NewsCreated = 0,
                    CommentsPosted = 0,
                    AuditLogsGenerated = 0
                })
                .ToListAsync();
        }
    }
}