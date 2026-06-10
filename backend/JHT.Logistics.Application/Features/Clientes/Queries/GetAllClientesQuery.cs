using JHT.Logistics.Application.Features.Clientes.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Clientes.Queries;

public class GetAllClientesQuery : IRequest<IEnumerable<ClienteDto>>
{
}

public class GetAllClientesQueryHandler : IRequestHandler<GetAllClientesQuery, IEnumerable<ClienteDto>>
{
    private readonly IClienteRepository _repository;

    public GetAllClientesQueryHandler(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ClienteDto>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
    {
        var clientes = await _repository.GetAllAsync(cancellationToken);
        return clientes.Select(c => c.ToDto());
    }
}
