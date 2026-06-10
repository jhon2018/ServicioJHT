using JHT.Logistics.Application.Features.Clientes.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Clientes.Queries;

public class GetClienteByIdQuery : IRequest<ClienteDto?>
{
    public int CliId { get; set; }
}

public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, ClienteDto?>
{
    private readonly IClienteRepository _repository;

    public GetClienteByIdQueryHandler(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClienteDto?> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
    {
        var cliente = await _repository.GetByIdAsync(request.CliId, cancellationToken);
        return cliente?.ToDto();
    }
}
