using JHT.Logistics.Application.Features.Vehiculos.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Vehiculos.Queries;

public class GetVehiculoByIdQuery : IRequest<VehiculoDto?>
{
    public int VehId { get; set; }
}

public class GetVehiculoByIdQueryHandler : IRequestHandler<GetVehiculoByIdQuery, VehiculoDto?>
{
    private readonly IVehiculoRepository _repository;

    public GetVehiculoByIdQueryHandler(IVehiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<VehiculoDto?> Handle(GetVehiculoByIdQuery request, CancellationToken cancellationToken)
    {
        var vehiculo = await _repository.GetByIdAsync(request.VehId, cancellationToken);
        return vehiculo?.ToDto();
    }
}
