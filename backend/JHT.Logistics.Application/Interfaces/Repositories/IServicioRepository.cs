using JHT.Logistics.Domain.Entities;

namespace JHT.Logistics.Application.Interfaces.Repositories;

public interface IServicioRepository
{
    Task<Servicio?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Servicio>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Servicio>> GetAllWithStatesAsync(CancellationToken cancellationToken = default);
    Task<Servicio> AddAsync(Servicio servicio, CancellationToken cancellationToken = default);
    Task UpdateAsync(Servicio servicio, CancellationToken cancellationToken = default);
    
    Task<TTrackingPublico?> GetTrackingByTokenAsync(string token, CancellationToken cancellationToken = default);
    
    // Podrían agregarse métodos específicos para Destinos, Documentos, Asignaciones
}
