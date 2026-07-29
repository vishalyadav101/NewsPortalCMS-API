using NewsPortalCMS.Application.DTOs.Media;

namespace NewsPortalCMS.Application.Interfaces.Services;

public interface IMediaService
{
    Task<MediaResponseDto> CreateAsync(CreateMediaDto dto);

    Task<IEnumerable<MediaResponseDto>> GetAllAsync();

    Task<MediaResponseDto?> GetByIdAsync(int id);

    Task<bool> UpdateAsync(UpdateMediaDto dto);

    Task<bool> DeleteAsync(int id);
}