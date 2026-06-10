using JHT.Logistics.Application.Features.Auth.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using JHT.Logistics.Application.Interfaces.Services;
using MediatR;

namespace JHT.Logistics.Application.Features.Auth.Queries;

public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public LoginQueryHandler(
        IUsuarioRepository usuarioRepository, 
        IPasswordHasher passwordHasher, 
        ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.GetByUsernameAsync(request.Username);

        if (usuario == null)
            throw new UnauthorizedAccessException("Credenciales incorrectas.");

        if (!_passwordHasher.VerifyPassword(request.Password, usuario.UsuPasswordHash))
            throw new UnauthorizedAccessException("Credenciales incorrectas.");

        var roles = usuario.UsuariosRoles.Select(r => r.Rol.RolNombre).ToList();
        var token = _tokenService.GenerateToken(usuario, roles);

        return new LoginResponse
        {
            AccessToken = token,
            ExpiresIn = 3600, // 1 hour
            Roles = roles
        };
    }
}
