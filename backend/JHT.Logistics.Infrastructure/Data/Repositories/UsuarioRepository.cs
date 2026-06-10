using JHT.Logistics.Application.Interfaces.Repositories;
using JHT.Logistics.Domain.Entities.Security;
using Microsoft.EntityFrameworkCore;

namespace JHT.Logistics.Infrastructure.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly JhtDbContext _context;

    public UsuarioRepository(JhtDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByUsernameAsync(string username)
    {
        return await _context.Set<Usuario>()
            .Include(u => u.UsuariosRoles)
                .ThenInclude(ur => ur.Rol)
            .FirstOrDefaultAsync(u => u.UsuLogin == username && u.Estado);
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Set<Usuario>()
            .Include(u => u.UsuariosRoles)
                .ThenInclude(ur => ur.Rol)
            .FirstOrDefaultAsync(u => u.UsuEmail == email && u.Estado);
    }
}
