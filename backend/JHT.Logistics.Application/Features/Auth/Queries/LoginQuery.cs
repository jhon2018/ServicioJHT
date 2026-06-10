using JHT.Logistics.Application.Features.Auth.DTOs;
using MediatR;

namespace JHT.Logistics.Application.Features.Auth.Queries;

public class LoginQuery : IRequest<LoginResponse>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public LoginQuery(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
