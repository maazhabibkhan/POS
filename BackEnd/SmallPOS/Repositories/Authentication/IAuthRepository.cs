using SmallPOS.API.Models.Requests;
using SmallPOS.API.Models.Responses;

namespace SmallPOS.API.Repositories.Authentication;

public interface IAuthRepository
{
    Task<LoginResponse?> GetByUsernameAsync(string username);
    Task<RegisterResponse?> RegisterAsync(RegisterRequest request, string passwordHash);
}
