using JHT.Logistics.Application.Features.Vehiculos.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Vehiculos.Queries;

public class GetAllVehiculosQuery : IRequest<IEnumerable<VehiculoDto>>
{
}

public class GetAllVehiculosQueryHandler : IRequestHandler<GetAllVehiculosQuery, IEnumerable<VehiculoDto>>
{
    private readonly IVehiculoRepository _repository;

    public GetAllVehiculosQueryHandler(IVehiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<VehiculoDto>> Handle(GetAllVehiculosQuery request, CancellationToken cancellationToken)
    {
        var vehiculos = await _repository.GetAllAsync(cancellationToken);
        return vehiculos.Select(v => v.ToDto());
    }
}
