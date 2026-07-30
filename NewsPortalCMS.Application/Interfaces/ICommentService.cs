using NewsPortalCMS.Application.DTOs.Comment;

namespace NewsPortalCMS.Application.Interfaces.Services;

public interface ICommentService
{
    Task<IEnumerable<CommentResponseDto>> GetAllAsync();

    Task<CommentResponseDto?> GetByIdAsync(Guid id);

    Task<IEnumerable<CommentResponseDto>> GetByNewsIdAsync(int newsId);

    Task<CommentResponseDto> CreateAsync(CreateCommentDto createCommentDto);

    Task<bool> UpdateAsync(Guid id, UpdateCommentDto updateCommentDto);

    Task<bool> DeleteAsync(Guid id);
}