using AutoMapper;
using NewsPortalCMS.Application.DTOs.Comment;
using NewsPortalCMS.Application.Interfaces.Repositories;
using NewsPortalCMS.Application.Interfaces.Services;
using NewsPortalCMS.Domain.Entities;

namespace NewsPortalCMS.Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(
        ICommentRepository commentRepository,
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CommentResponseDto>> GetAllAsync()
    {
        var comments = await _commentRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<CommentResponseDto>>(comments);
    }

    public async Task<CommentResponseDto?> GetByIdAsync(Guid id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);

        if (comment == null)
            return null;

        return _mapper.Map<CommentResponseDto>(comment);
    }

    public async Task<IEnumerable<CommentResponseDto>> GetByNewsIdAsync(int newsId)
    {
        var comments = await _commentRepository.GetByNewsIdAsync(newsId);

        return _mapper.Map<IEnumerable<CommentResponseDto>>(comments);
    }

    public async Task<CommentResponseDto> CreateAsync(CreateCommentDto createCommentDto)
    {
        var comment = _mapper.Map<Comment>(createCommentDto);

        comment.CreatedDate = DateTime.UtcNow;
        comment.IsApproved = false;
        comment.IsActive = true;

        await _commentRepository.AddAsync(comment);

        return _mapper.Map<CommentResponseDto>(comment);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateCommentDto updateCommentDto)
    {
        var comment = await _commentRepository.GetByIdAsync(id);

        if (comment == null)
            return false;

        _mapper.Map(updateCommentDto, comment);

        comment.UpdatedDate = DateTime.UtcNow;

        await _commentRepository.UpdateAsync(comment);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);

        if (comment == null)
            return false;

        await _commentRepository.DeleteAsync(comment);

        return true;
    }
}