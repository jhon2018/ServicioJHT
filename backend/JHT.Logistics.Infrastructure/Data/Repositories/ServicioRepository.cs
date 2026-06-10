using JHT.Logistics.Application.Interfaces.Repositories;
using JHT.Logistics.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JHT.Logistics.Infrastructure.Data.Repositories;

public class ServicioRepository : IServicioRepository
{
    private readonly JhtDbContext _context;

    public ServicioRepository(JhtDbContext context)
    {
        _context = context;
    }

    public async Task<Servicio?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Servicios
            .Include(s => s.Destinos)
            .Include(s => s.Documentos)
            .Include(s => s.HistorialEstados).ThenInclude(h => h.EstadoServicio)
            .Include(s => s.ConductoresAsignados).ThenInclude(c => c.Conductor)
            .Include(s => s.VehiculosAsignados).ThenInclude(v => v.Vehiculo)
            .Include(s => s.TrackingPublico)
            .Include(s => s.EstadoServicio)
            .Include(s => s.Cliente)
            .FirstOrDefaultAsync(s => s.SerId == id, cancellationToken);
    }

    public async Task<IEnumerable<Servicio>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Servicios
            .Include(s => s.Cliente)
            .Include(s => s.EstadoServicio)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Servicio>> GetAllWithStatesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Servicio>()
            .Include(s => s.EstadoServicio)
            .ToListAsync(cancellationToken);
    }

    public async Task<Servicio> AddAsync(Servicio servicio, CancellationToken cancellationToken = default)
    {
        await _context.Servicios.AddAsync(servicio, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return servicio;
    }

    public async Task UpdateAsync(Servicio servicio, CancellationToken cancellationToken = default)
    {
        _context.Servicios.Update(servicio);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TTrackingPublico?> GetTrackingByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return await _context.TrackingPublico
            .Include(t => t.Servicio)
                .ThenInclude(s => s.Destinos)
            .Include(t => t.Servicio)
                .ThenInclude(s => s.HistorialEstados)
                    .ThenInclude(h => h.EstadoServicio)
            .Include(t => t.Servicio)
                .ThenInclude(s => s.EstadoServicio)
            .FirstOrDefaultAsync(t => t.TrkToken == token && t.TrkEstado, cancellationToken);
    }
}
