using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> GetAllAsync();

    Task<Comment?> GetByIdAsync(Guid id);

    Task<IEnumerable<Comment>> GetByNewsIdAsync(int newsId);

    Task AddAsync(Comment comment);

    Task UpdateAsync(Comment comment);

    Task DeleteAsync(Comment comment);

    Task<bool> ExistsAsync(Guid id);
}