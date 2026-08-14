using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsPortalCMS.Application.DTOs.Auth;
using NewsPortalCMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace NewsPortalCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {
            var result = await _authService.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var result = await _authService.LoginAsync(model);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            return Ok("You are authenticated.");
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("superadmin")]
        public IActionResult SuperAdmin()
        {
            return Ok("Welcome SuperAdmin");
        }
    }
}
