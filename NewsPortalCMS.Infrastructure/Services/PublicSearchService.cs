using NewsPortalCMS.Application.DTOs.Public.Search;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Application.Services
{
    public class PublicSearchService : IPublicSearchService
    {
        private readonly IPublicSearchRepository _searchRepository;

        public PublicSearchService(IPublicSearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public async Task<PagedResultDto<PublicSearchResultDto>> SearchNewsAsync(
            string keyword,
            int pageNumber,
            int pageSize)
        {
            if (pageNumber <= 0)
                pageNumber = 1;

            if (pageSize <= 0)
                pageSize = 10;

            var result = await _searchRepository.SearchNewsAsync(
                keyword,
                pageNumber,
                pageSize);

            var newsDtos = result.News.Select(news => new PublicSearchResultDto
            {
                Id = news.Id,
                Title = news.Title,
                Slug = news.Slug,
                ShortDescription = news.ShortDescription,
                FeaturedImage = news.FeaturedImage,
                Author = news.Author,
                PublishDate = news.PublishDate,
                CategoryName = news.Category != null
                    ? news.Category.Name
                    : string.Empty
            }).ToList();

            return new PagedResultDto<PublicSearchResultDto>
            {
                TotalRecords = result.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = newsDtos
            };
        }
    }
}