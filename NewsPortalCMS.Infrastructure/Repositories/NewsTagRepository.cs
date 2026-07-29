using Microsoft.EntityFrameworkCore;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Domain.Entities;
using NewsPortalCMS.Infrastructure.Data;

namespace NewsPortalCMS.Infrastructure.Repositories
{
    public class NewsTagRepository : INewsTagRepository
    {
        private readonly ApplicationDbContext _context;

        public NewsTagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<NewsTag>> GetByNewsIdAsync(int newsId)
        {
            return await _context.NewsTags
                .Where(nt => nt.NewsId == newsId)
                .Include(nt => nt.Tag)
                .ToListAsync();
        }

        public async Task ReplaceNewsTagsAsync(
            int newsId,
            IEnumerable<int> tagIds)
        {
            // Existing tags remove
            var existingTags = await _context.NewsTags
                .Where(nt => nt.NewsId == newsId)
                .ToListAsync();

            if (existingTags.Count > 0)
            {
                _context.NewsTags.RemoveRange(existingTags);
            }

            // Duplicate TagIds remove
            var uniqueTagIds = tagIds.Distinct();

            // New tags assign
            foreach (var tagId in uniqueTagIds)
            {
                _context.NewsTags.Add(new NewsTag
                {
                    NewsId = newsId,
                    TagId = tagId
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}