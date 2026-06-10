using JHT.Logistics.Application.Interfaces.Repositories;
using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JHT.Logistics.Infrastructure.Data.Repositories;

public class ConductorRepository : IConductorRepository
{
    private readonly JhtDbContext _context;

    public ConductorRepository(JhtDbContext context)
    {
        _context = context;
    }

    public async Task<Conductor?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Conductores.FirstOrDefaultAsync(c => c.ConId == id, cancellationToken);
    }

    public async Task<IEnumerable<Conductor>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Conductores.ToListAsync(cancellationToken);
    }

    public async Task<Conductor> AddAsync(Conductor conductor, CancellationToken cancellationToken = default)
    {
        await _context.Conductores.AddAsync(conductor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return conductor;
    }

    public async Task UpdateAsync(Conductor conductor, CancellationToken cancellationToken = default)
    {
        _context.Conductores.Update(conductor);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Conductor conductor, CancellationToken cancellationToken = default)
    {
        _context.Conductores.Remove(conductor);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
