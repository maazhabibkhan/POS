using SmallPOS.API.Models;
using SmallPOS.API.Models.Requests;

namespace SmallPOS.API.Repositories.Authentication;

public interface IAuthRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> RegisterAsync(RegisterRequest request, string passwordHash);
}
