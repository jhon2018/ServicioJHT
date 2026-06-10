namespace JHT.Logistics.Application.Features.Auth.DTOs;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public List<string> Roles { get; set; } = new();
}
