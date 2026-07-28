using NewsPortalCMS.Application.DTOs.Tag;

namespace NewsPortalCMS.Application.Interfaces;

public interface ITagService
{
    Task<List<TagResponseDto>> GetAllAsync();

    Task<TagResponseDto?> GetByIdAsync(int id);

    Task<TagResponseDto> CreateAsync(TagCreateDto model);

    Task<bool> UpdateAsync(int id, TagUpdateDto model);

    Task<bool> DeleteAsync(int id);
}