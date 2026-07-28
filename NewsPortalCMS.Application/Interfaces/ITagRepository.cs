using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces;

public interface ITagRepository
{
    Task<List<Tag>> GetAllAsync();

    Task<Tag?> GetByIdAsync(int id);

    Task<Tag> CreateAsync(Tag tag);

    Task UpdateAsync(Tag tag);

    Task DeleteAsync(Tag tag);

    Task<bool> SlugExistsAsync(string slug);

    Task<bool> SlugExistsAsync(
        string slug,
        int excludeTagId);
}