using NewsPortalCMS.Application.Common.Pagination;
using NewsPortalCMS.DTOs.News;

namespace NewsPortalCMS.Services.Interfaces
{
    public interface INewsService
    {
        Task<PaginatedResponse<NewsDto>> GetAllAsync(
     NewsQueryRequest request);
        Task<NewsDto?> GetByIdAsync(int id);

        Task<NewsDto> CreateAsync(CreateNewsDto dto);

        Task<NewsDto?> UpdateAsync(UpdateNewsDto dto);

        Task<bool> DeleteAsync(int id);
    }
}