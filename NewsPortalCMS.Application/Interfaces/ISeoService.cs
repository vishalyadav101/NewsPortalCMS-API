using NewsPortalCMS.Application.DTOs.Seo;

namespace NewsPortalCMS.Application.Interfaces.Services
{
    public interface ISeoService
    {
        Task<IEnumerable<SeoResponseDto>> GetAllAsync();

        Task<SeoResponseDto?> GetByIdAsync(int id);

        Task<SeoResponseDto?> GetByPageNameAsync(string pageName);

        Task<SeoResponseDto> CreateAsync(CreateSeoDto createSeoDto);

        Task<SeoResponseDto?> UpdateAsync(UpdateSeoDto updateSeoDto);

        Task<bool> DeleteAsync(int id);
    }
}