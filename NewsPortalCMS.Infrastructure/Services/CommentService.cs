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


    // ==========================================
    // GET ALL
    // ==========================================

    public async Task<IEnumerable<CommentResponseDto>> GetAllAsync()
    {
        var comments = await _commentRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<CommentResponseDto>>(comments);
    }


    // ==========================================
    // GET BY ID
    // ==========================================

    public async Task<CommentResponseDto?> GetByIdAsync(Guid id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);

        if (comment == null)
            return null;

        return _mapper.Map<CommentResponseDto>(comment);
    }


    // ==========================================
    // GET BY NEWS ID
    // ==========================================

    public async Task<IEnumerable<CommentResponseDto>> GetByNewsIdAsync(int newsId)
    {
        var comments = await _commentRepository.GetByNewsIdAsync(newsId);

        return _mapper.Map<IEnumerable<CommentResponseDto>>(comments);
    }


    // ==========================================
    // CREATE COMMENT
    // ==========================================

    public async Task<CommentResponseDto> CreateAsync(
        CreateCommentDto createCommentDto)
    {
        var comment = _mapper.Map<Comment>(createCommentDto);

        // ======================================
        // SYSTEM VALUES
        // ======================================

        comment.CreatedDate = DateTime.UtcNow;

        // Automatically approve new comments
        comment.IsApproved = true;

        // Automatically make comment active
        comment.IsActive = true;


        // ======================================
        // SAVE
        // ======================================

        await _commentRepository.AddAsync(comment);


        // ======================================
        // RESPONSE
        // ======================================

        return _mapper.Map<CommentResponseDto>(comment);
    }


    // ==========================================
    // UPDATE COMMENT
    // ==========================================

    public async Task<bool> UpdateAsync(
        Guid id,
        UpdateCommentDto updateCommentDto)
    {
        var comment =
            await _commentRepository.GetByIdAsync(id);

        if (comment == null)
            return false;


        _mapper.Map(
            updateCommentDto,
            comment
        );


        comment.UpdatedDate =
            DateTime.UtcNow;


        await _commentRepository.UpdateAsync(
            comment
        );


        return true;
    }


    // ==========================================
    // DELETE COMMENT
    // ==========================================

    public async Task<bool> DeleteAsync(Guid id)
    {
        var comment =
            await _commentRepository.GetByIdAsync(id);

        if (comment == null)
            return false;


        await _commentRepository.DeleteAsync(
            comment
        );


        return true;
    }
}