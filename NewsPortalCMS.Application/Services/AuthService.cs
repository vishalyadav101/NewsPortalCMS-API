using NewsPortalCMS.Application.DTOs.Auth;
using NewsPortalCMS.Application.Interfaces;

namespace NewsPortalCMS.Application.Services;

public class AuthService : IAuthService
{
    public async Task<string> RegisterAsync(RegisterDto model)
    {
        throw new NotImplementedException();
    }

    public async Task<string> LoginAsync(LoginDto model)
    {
        throw new NotImplementedException();
    }
}