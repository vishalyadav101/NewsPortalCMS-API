using NewsPortalCMS.Application.DTOs.NewsTag;
using NewsPortalCMS.Application.Interfaces;

namespace NewsPortalCMS.Application.Services
{
    public class NewsTagService : INewsTagService
    {
        private readonly INewsTagRepository _newsTagRepository;

        public NewsTagService(INewsTagRepository newsTagRepository)
        {
            _newsTagRepository = newsTagRepository;
        }

        public async Task<List<int>> GetTagIdsByNewsIdAsync(int newsId)
        {
            var newsTags = await _newsTagRepository.GetByNewsIdAsync(newsId);

            return newsTags
                .Select(x => x.TagId)
                .ToList();
        }

        public async Task AssignTagsAsync(AssignNewsTagsDto dto)
        {
            await _newsTagRepository.ReplaceNewsTagsAsync(
                dto.NewsId,
                dto.TagIds
            );
        }
    }
}