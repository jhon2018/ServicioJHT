using JHT.Logistics.Domain.Entities.Security;

namespace JHT.Logistics.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(Usuario usuario, IList<string> roles);
}
