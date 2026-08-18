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
        [FromBody] WebsiteSettingCreateDto model)
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
        [FromBody] WebsiteSettingUpdateDto model)
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
                message =
                    "Website setting updated successfully."
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
            message =
                "Website setting deleted successfully."
        });
    }


    // POST: api/WebsiteSetting/1/logo
    [HttpPost("{id:int}/logo")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadLogo(
        int id,
        IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new
            {
                message = "Logo file is required."
            });
        }

        try
        {
            var result =
                await _websiteSettingService.UploadLogoAsync(
                    id,
                    file);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Website setting not found."
                });
            }

            return Ok(new
            {
                message =
                    "Logo uploaded successfully.",
                logoUrl = result
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


    // POST: api/WebsiteSetting/1/favicon
    [HttpPost("{id:int}/favicon")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFavicon(
        int id,
        IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new
            {
                message = "Favicon file is required."
            });
        }

        try
        {
            var result =
                await _websiteSettingService.UploadFaviconAsync(
                    id,
                    file);

            if (result == null)
            {
                return NotFound(new
                {
                    message =
                        "Website setting not found."
                });
            }

            return Ok(new
            {
                message =
                    "Favicon uploaded successfully.",
                faviconUrl = result
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
}