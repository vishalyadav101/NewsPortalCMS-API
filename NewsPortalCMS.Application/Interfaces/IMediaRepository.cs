using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories;

public interface IMediaRepository
{
    Task<Media> AddAsync(Media media);

    Task<Media?> GetByIdAsync(int id);

    Task<IEnumerable<Media>> GetAllAsync();

    Task UpdateAsync(Media media);

    Task DeleteAsync(Media media);
}