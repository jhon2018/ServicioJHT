using JHT.Logistics.Domain.Entities;

namespace JHT.Logistics.Application.Interfaces.Repositories;

public interface IVehiculoRepository
{
    Task<Vehiculo?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Vehiculo>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Vehiculo> AddAsync(Vehiculo vehiculo, CancellationToken cancellationToken = default);
    Task UpdateAsync(Vehiculo vehiculo, CancellationToken cancellationToken = default);
    Task DeleteAsync(Vehiculo vehiculo, CancellationToken cancellationToken = default);
}
