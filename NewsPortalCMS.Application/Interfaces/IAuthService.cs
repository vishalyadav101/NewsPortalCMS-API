using NewsPortalCMS.Application.DTOs.Auth;

namespace NewsPortalCMS.Application.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto model);

    Task<string> LoginAsync(LoginDto model);
}