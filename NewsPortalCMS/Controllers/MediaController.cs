using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.Media;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MediaController : ControllerBase
{
    private readonly IMediaService _mediaService;

    public MediaController(IMediaService mediaService)
    {
        _mediaService = mediaService;
    }


    // POST: api/Media
    [HttpPost]
    public async Task<IActionResult> Create(CreateMediaDto dto)
    {
        var result = await _mediaService.CreateAsync(dto);

        return Ok(new
        {
            message = "Media created successfully",
            data = result
        });
    }


    // GET: api/Media
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediaService.GetAllAsync();

        return Ok(result);
    }


    // GET: api/Media/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediaService.GetByIdAsync(id);

        if (result == null)
            return NotFound(new
            {
                message = "Media not found"
            });


        return Ok(result);
    }


    // PUT: api/Media
    [HttpPut]
    public async Task<IActionResult> Update(UpdateMediaDto dto)
    {
        var result = await _mediaService.UpdateAsync(dto);

        if (!result)
            return NotFound(new
            {
                message = "Media not found"
            });


        return Ok(new
        {
            message = "Media updated successfully"
        });
    }


    // DELETE: api/Media/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediaService.DeleteAsync(id);

        if (!result)
            return NotFound(new
            {
                message = "Media not found"
            });


        return Ok(new
        {
            message = "Media deleted successfully"
        });
    }
}