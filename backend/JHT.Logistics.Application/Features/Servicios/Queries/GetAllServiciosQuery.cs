using JHT.Logistics.Application.Features.Servicios.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Servicios.Queries;

public class GetAllServiciosQuery : IRequest<IEnumerable<ServicioDto>>
{
}

public class GetAllServiciosQueryHandler : IRequestHandler<GetAllServiciosQuery, IEnumerable<ServicioDto>>
{
    private readonly IServicioRepository _repository;

    public GetAllServiciosQueryHandler(IServicioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ServicioDto>> Handle(GetAllServiciosQuery request, CancellationToken cancellationToken)
    {
        var servicios = await _repository.GetAllAsync(cancellationToken);
        return servicios.Select(s => s.ToDto());
    }
}
