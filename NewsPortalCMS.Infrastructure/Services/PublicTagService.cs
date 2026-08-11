using NewsPortalCMS.Application.DTOs.PublicTag;
using NewsPortalCMS.Application.Interfaces;

namespace NewsPortalCMS.Infrastructure.Services;

public class PublicTagService : IPublicTagService
{
    private readonly ITagRepository _tagRepository;

    public PublicTagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    // Get all active tags
    public async Task<List<PublicTagResponseDto>> GetAllAsync()
    {
        var tags = await _tagRepository.GetAllAsync();

        return tags
            .Where(tag => tag.IsActive)
            .Select(tag => new PublicTagResponseDto
            {
                Id = tag.Id,
                Name = tag.Name,
                Slug = tag.Slug
            })
            .ToList();
    }

    // Get active tag by slug
    public async Task<PublicTagResponseDto?> GetBySlugAsync(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return null;
        }

        var normalizedSlug = slug.Trim().ToLowerInvariant();

        var tag = await _tagRepository.GetBySlugAsync(normalizedSlug);

        if (tag == null)
        {
            return null;
        }

        return new PublicTagResponseDto
        {
            Id = tag.Id,
            Name = tag.Name,
            Slug = tag.Slug
        };
    }
}