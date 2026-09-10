using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SmallPOS.API.Models;
using SmallPOS.API.Models.Requests;
using SmallPOS.API.Models.Responses;
using SmallPOS.API.Repositories.Authentication;

namespace SmallPOS.API.Services.Authentication;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IConfiguration _configuration;

    public AuthService(
        IAuthRepository authRepository,
        IConfiguration configuration)
    {
        _authRepository = authRepository;
        _configuration = configuration;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _authRepository.GetByUsernameAsync(request.Username);

        if (user == null || !user.IsActive)
        {
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return null;
        }

        var token = CreateToken(user);

        return new LoginResponse
        {
            Token = token,
            UserId = user.Id,
            Username = user.Username,
            RoleId = user.RoleId,
            RoleName = user.RoleName
        };
    }

    public async Task<RegisterResponse?> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _authRepository.GetByUsernameAsync(request.Username);

        if (existingUser != null)
        {
            return null;
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = await _authRepository.RegisterAsync(request, passwordHash);

        if (user == null)
        {
            return null;
        }

        return new RegisterResponse
        {
            UserId = user.Id,
            Username = user.Username,
            RoleId = user.RoleId,
            RoleName = user.RoleName,
            IsActive = user.IsActive
        };
    }

    private string CreateToken(User user)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = jwtSettings["Key"] ?? throw new InvalidOperationException("JWT key is missing.");
        var issuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("JWT issuer is missing.");
        var audience = jwtSettings["Audience"] ?? throw new InvalidOperationException("JWT audience is missing.");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.RoleName)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
