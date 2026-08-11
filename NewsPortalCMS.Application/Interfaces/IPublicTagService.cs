using NewsPortalCMS.Application.DTOs.PublicTag;

namespace NewsPortalCMS.Application.Interfaces;

public interface IPublicTagService
{
    Task<List<PublicTagResponseDto>> GetAllAsync();

    Task<PublicTagResponseDto?> GetBySlugAsync(string slug);
}