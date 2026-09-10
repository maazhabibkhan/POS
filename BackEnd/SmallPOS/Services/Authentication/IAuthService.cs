using SmallPOS.API.Models.Requests;
using SmallPOS.API.Models.Responses;

namespace SmallPOS.API.Services.Authentication;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
    Task<RegisterResponse?> RegisterAsync(RegisterRequest request);
}
