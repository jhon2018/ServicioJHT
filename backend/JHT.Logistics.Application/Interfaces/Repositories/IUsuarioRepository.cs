using JHT.Logistics.Domain.Entities.Security;

namespace JHT.Logistics.Application.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByUsernameAsync(string username);
    Task<Usuario?> GetByEmailAsync(string email);
}
