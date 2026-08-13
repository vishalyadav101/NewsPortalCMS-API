using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.WebsiteSetting;
using NewsPortalCMS.Application.Interfaces.Services;

namespace NewsPortalCMS.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class WebsiteSettingController : ControllerBase
{
    private readonly IWebsiteSettingService _websiteSettingService;


    public WebsiteSettingController(
        IWebsiteSettingService websiteSettingService)
    {
        _websiteSettingService = websiteSettingService;
    }


    // GET: api/WebsiteSetting
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var setting =
            await _websiteSettingService.GetAsync();


        if (setting == null)
        {
            return NotFound(new
            {
                message = "Website setting not found."
            });
        }


        return Ok(setting);
    }



    // GET: api/WebsiteSetting/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var setting =
            await _websiteSettingService.GetByIdAsync(id);


        if (setting == null)
        {
            return NotFound(new
            {
                message = "Website setting not found."
            });
        }


        return Ok(setting);
    }



    // POST: api/WebsiteSetting
    [HttpPost]
    public async Task<IActionResult> Create(
        WebsiteSettingCreateDto model)
    {
        try
        {
            var result =
                await _websiteSettingService.CreateAsync(model);


            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    id = result.Id
                },
                result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }



    // PUT: api/WebsiteSetting/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        WebsiteSettingUpdateDto model)
    {
        try
        {
            var result =
                await _websiteSettingService.UpdateAsync(
                    id,
                    model);


            if (!result)
            {
                return NotFound(new
                {
                    message = "Website setting not found."
                });
            }


            return Ok(new
            {
                message = "Website setting updated successfully."
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }



    // DELETE: api/WebsiteSetting/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result =
            await _websiteSettingService.DeleteAsync(id);


        if (!result)
        {
            return NotFound(new
            {
                message = "Website setting not found."
            });
        }


        return Ok(new
        {
            message = "Website setting deleted successfully."
        });
    }
}