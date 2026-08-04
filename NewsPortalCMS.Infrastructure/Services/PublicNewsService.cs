using AutoMapper;
using NewsPortalCMS.Application.DTOs.Public;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Application.Services
{
    public class PublicNewsService : IPublicNewsService
    {
        private readonly IPublicNewsRepository _repository;
        private readonly IMapper _mapper;

        public PublicNewsService(
            IPublicNewsRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PublicNewsDto>> GetLatestNewsAsync(int count)
        {
            var news = await _repository.GetLatestNewsAsync(count);

            return _mapper.Map<IEnumerable<PublicNewsDto>>(news);
        }

        public async Task<IEnumerable<PublicNewsDto>> GetFeaturedNewsAsync(int count)
        {
            var news = await _repository.GetFeaturedNewsAsync(count);

            return _mapper.Map<IEnumerable<PublicNewsDto>>(news);
        }

        public async Task<IEnumerable<PublicNewsDto>> GetPopularNewsAsync(int count)
        {
            var news = await _repository.GetPopularNewsAsync(count);

            return _mapper.Map<IEnumerable<PublicNewsDto>>(news);
        }

        public async Task<IEnumerable<PublicNewsDto>> GetNewsByCategoryAsync(int categoryId)
        {
            var news = await _repository.GetNewsByCategoryAsync(categoryId);

            return _mapper.Map<IEnumerable<PublicNewsDto>>(news);
        }

        public async Task<IEnumerable<PublicNewsDto>> SearchNewsAsync(string keyword)
        {
            var news = await _repository.SearchNewsAsync(keyword);

            return _mapper.Map<IEnumerable<PublicNewsDto>>(news);
        }

        public async Task<PublicNewsDetailsDto?> GetNewsBySlugAsync(string slug)
        {
            var news = await _repository.GetNewsBySlugAsync(slug);

            if (news == null)
                return null;

            var dto = _mapper.Map<PublicNewsDetailsDto>(news);

            dto.Tags = news.NewsTags
                .Select(nt => nt.Tag.Name)
                .ToList();

            dto.Comments = news.Comments
                .Where(c => c.IsApproved && c.IsActive)
                .OrderByDescending(c => c.CreatedDate)
                .Select(c => new PublicCommentDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Content = c.Content,
                    CreatedDate = c.CreatedDate
                })
                .ToList();

            return dto;
        }
    }
}