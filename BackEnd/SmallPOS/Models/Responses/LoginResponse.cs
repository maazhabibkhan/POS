using System.Text.Json.Serialization;

namespace SmallPOS.API.Models.Responses;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;

    // Used internally by the authentication service to verify the password.
    // JsonIgnore makes sure the password hash is never sent to the frontend.
    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}
