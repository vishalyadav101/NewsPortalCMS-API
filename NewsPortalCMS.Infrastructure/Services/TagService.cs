using NewsPortalCMS.Application.DTOs.Tag;
using NewsPortalCMS.Application.Interfaces;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Infrastructure.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    // Saare tags get karega
    public async Task<List<TagResponseDto>> GetAllAsync()
    {
        var tags = await _tagRepository.GetAllAsync();

        return tags.Select(tag => new TagResponseDto
        {
            Id = tag.Id,
            Name = tag.Name,
            Slug = tag.Slug,
            IsActive = tag.IsActive,
            CreatedDate = tag.CreatedDate,
            UpdatedDate = tag.UpdatedDate
        }).ToList();
    }

    // ID se single tag
    public async Task<TagResponseDto?> GetByIdAsync(int id)
    {
        var tag = await _tagRepository.GetByIdAsync(id);

        if (tag == null)
        {
            return null;
        }

        return new TagResponseDto
        {
            Id = tag.Id,
            Name = tag.Name,
            Slug = tag.Slug,
            IsActive = tag.IsActive,
            CreatedDate = tag.CreatedDate,
            UpdatedDate = tag.UpdatedDate
        };
    }

    // New tag create
    public async Task<TagResponseDto> CreateAsync(TagCreateDto model)
    {
        var slug = model.Slug.Trim().ToLowerInvariant();

        // Duplicate slug allow nahi karenge
        if (await _tagRepository.SlugExistsAsync(slug))
        {
            throw new InvalidOperationException(
                "Tag with this slug already exists.");
        }

        var tag = new Tag
        {
            Name = model.Name.Trim(),
            Slug = slug,
            IsActive = model.IsActive,
            CreatedDate = DateTime.UtcNow
        };

        var createdTag =
            await _tagRepository.CreateAsync(tag);

        return new TagResponseDto
        {
            Id = createdTag.Id,
            Name = createdTag.Name,
            Slug = createdTag.Slug,
            IsActive = createdTag.IsActive,
            CreatedDate = createdTag.CreatedDate,
            UpdatedDate = createdTag.UpdatedDate
        };
    }

    // Existing tag update
    public async Task<bool> UpdateAsync(
        int id,
        TagUpdateDto model)
    {
        var tag = await _tagRepository.GetByIdAsync(id);

        if (tag == null)
        {
            return false;
        }

        var slug = model.Slug.Trim().ToLowerInvariant();

        // Current Tag ko exclude karke duplicate check
        if (await _tagRepository.SlugExistsAsync(slug, id))
        {
            throw new InvalidOperationException(
                "Tag with this slug already exists.");
        }

        tag.Name = model.Name.Trim();
        tag.Slug = slug;
        tag.IsActive = model.IsActive;
        tag.UpdatedDate = DateTime.UtcNow;

        await _tagRepository.UpdateAsync(tag);

        return true;
    }

    // Tag delete
    public async Task<bool> DeleteAsync(int id)
    {
        var tag = await _tagRepository.GetByIdAsync(id);

        if (tag == null)
        {
            return false;
        }

        await _tagRepository.DeleteAsync(tag);

        return true;
    }
}