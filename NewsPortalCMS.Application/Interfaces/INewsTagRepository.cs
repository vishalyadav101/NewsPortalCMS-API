using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces
{
    public interface INewsTagRepository
    {
        Task<List<NewsTag>> GetByNewsIdAsync(int newsId);

        Task ReplaceNewsTagsAsync(
            int newsId,
            IEnumerable<int> tagIds);
    }
}