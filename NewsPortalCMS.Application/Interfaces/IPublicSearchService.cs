using NewsPortalCMS.Application.DTOs.Public.Search;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface IPublicSearchService
    {
        Task<PagedResultDto<PublicSearchResultDto>> SearchNewsAsync(
            string keyword,
            int pageNumber,
            int pageSize);
    }
}