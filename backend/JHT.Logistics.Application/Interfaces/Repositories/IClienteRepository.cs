using JHT.Logistics.Domain.Entities;

namespace JHT.Logistics.Application.Interfaces.Repositories;

public interface IClienteRepository
{
    Task<Cliente?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Cliente>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Cliente> AddAsync(Cliente cliente, CancellationToken cancellationToken = default);
    Task UpdateAsync(Cliente cliente, CancellationToken cancellationToken = default);
    Task DeleteAsync(Cliente cliente, CancellationToken cancellationToken = default);
}
