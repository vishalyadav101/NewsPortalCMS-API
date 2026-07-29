using NewsPortalCMS.Application.DTOs.NewsTag;

namespace NewsPortalCMS.Application.Interfaces
{
    public interface INewsTagService
    {
        Task<List<int>> GetTagIdsByNewsIdAsync(int newsId);
        Task AssignTagsAsync(AssignNewsTagsDto dto);
    }
}