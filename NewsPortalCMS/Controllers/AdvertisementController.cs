using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.Advertisement;
using NewsPortalCMS.Application.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace NewsPortalCMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _service;

        public AdvertisementController(IAdvertisementService service)
        {
            _service = service;
        }

        // POST: api/Advertisement
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] CreateAdvertisementDto dto)
        {
            var result = await _service.CreateAsync(dto);

            return Ok(result);
        }

        // GET: api/Advertisement
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        // GET: api/Advertisement/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound(new
                {
                    message = "Advertisement not found."
                });

            return Ok(result);
        }

        // PUT: api/Advertisement/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromForm] UpdateAdvertisementDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);

            if (!result)
                return NotFound(new
                {
                    message = "Advertisement not found."
                });

            return Ok(new
            {
                message = "Advertisement updated successfully."
            });
        }

        // DELETE: api/Advertisement/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result)
                return NotFound(new
                {
                    message = "Advertisement not found."
                });

            return Ok(new
            {
                message = "Advertisement deleted successfully."
            });
        }
    }
}