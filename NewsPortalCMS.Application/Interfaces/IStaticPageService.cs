using NewsPortalCMS.Application.DTOs.StaticPage;

namespace NewsPortalCMS.Application.Interfaces.Services;

public interface IStaticPageService
{
    Task<IEnumerable<StaticPageResponseDto>> GetAllAsync();

    Task<StaticPageResponseDto?> GetByIdAsync(int id);

    Task<StaticPageResponseDto?> GetBySlugAsync(string slug);

    Task<StaticPageResponseDto> CreateAsync(CreateStaticPageDto dto);

    Task<bool> UpdateAsync(int id, UpdateStaticPageDto dto);

    Task<bool> DeleteAsync(int id);
}