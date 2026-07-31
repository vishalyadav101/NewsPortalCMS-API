using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.DTOs.Dashboard;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboardStatisticsAsync()
        {
            var dashboard = new DashboardDto
            {
                TotalUsers = await _context.Users.CountAsync(),

                TotalNews = await _context.News.CountAsync(),

                PublishedNews = await _context.News
    .CountAsync(x => x.IsPublished),

                DraftNews = await _context.News
    .CountAsync(x => !x.IsPublished),

                TotalCategories = await _context.Categories.CountAsync(),

                TotalSubCategories = await _context.SubCategories.CountAsync(),

                TotalTags = await _context.Tags.CountAsync(),

                TotalComments = await _context.Comments.CountAsync(),

                PendingComments = await _context.Comments
                    .CountAsync(x => !x.IsApproved),

                TotalAdvertisements = await _context.Advertisements.CountAsync(),

                ActiveAdvertisements = await _context.Advertisements
                    .CountAsync(x => x.IsActive),

                TotalStaticPages = await _context.StaticPages.CountAsync()
            };

            return dashboard;
        }
    }
}