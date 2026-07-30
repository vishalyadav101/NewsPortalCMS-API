using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.Comment;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }


    // GET: api/comment
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentService.GetAllAsync();

        return Ok(comments);
    }


    // GET: api/comment/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var comment = await _commentService.GetByIdAsync(id);

        if (comment == null)
            return NotFound(new { message = "Comment not found." });

        return Ok(comment);
    }


    // GET: api/comment/news/{newsId}
    [HttpGet("news/{newsId}")]
    public async Task<IActionResult> GetByNewsId(int newsId)
    {
        var comments = await _commentService.GetByNewsIdAsync(newsId);

        return Ok(comments);
    }


    // POST: api/comment
    [HttpPost]
    public async Task<IActionResult> Create(CreateCommentDto dto)
    {
        var comment = await _commentService.CreateAsync(dto);

        return Ok(comment);
    }


    // PUT: api/comment/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateCommentDto dto)
    {
        var result = await _commentService.UpdateAsync(id, dto);

        if (!result)
            return NotFound(new { message = "Comment not found." });

        return Ok(new { message = "Comment updated successfully." });
    }


    // DELETE: api/comment/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _commentService.DeleteAsync(id);

        if (!result)
            return NotFound(new { message = "Comment not found." });

        return Ok(new { message = "Comment deleted successfully." });
    }
}