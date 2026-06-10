using JHT.Logistics.Application.Interfaces.Repositories;
using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JHT.Logistics.Infrastructure.Data.Repositories;

public class VehiculoRepository : IVehiculoRepository
{
    private readonly JhtDbContext _context;

    public VehiculoRepository(JhtDbContext context)
    {
        _context = context;
    }

    public async Task<Vehiculo?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Vehiculos.FirstOrDefaultAsync(v => v.VehId == id, cancellationToken);
    }

    public async Task<IEnumerable<Vehiculo>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Vehiculos.ToListAsync(cancellationToken);
    }

    public async Task<Vehiculo> AddAsync(Vehiculo vehiculo, CancellationToken cancellationToken = default)
    {
        await _context.Vehiculos.AddAsync(vehiculo, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return vehiculo;
    }

    public async Task UpdateAsync(Vehiculo vehiculo, CancellationToken cancellationToken = default)
    {
        _context.Vehiculos.Update(vehiculo);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Vehiculo vehiculo, CancellationToken cancellationToken = default)
    {
        _context.Vehiculos.Remove(vehiculo);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
