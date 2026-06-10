using JHT.Logistics.Domain.Entities;

namespace JHT.Logistics.Application.Interfaces.Repositories;

public interface IConductorRepository
{
    Task<Conductor?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Conductor>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Conductor> AddAsync(Conductor conductor, CancellationToken cancellationToken = default);
    Task UpdateAsync(Conductor conductor, CancellationToken cancellationToken = default);
    Task DeleteAsync(Conductor conductor, CancellationToken cancellationToken = default);
}
