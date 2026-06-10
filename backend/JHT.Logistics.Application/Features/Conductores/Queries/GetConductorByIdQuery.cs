using JHT.Logistics.Application.Features.Conductores.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Conductores.Queries;

public class GetConductorByIdQuery : IRequest<ConductorDto?>
{
    public int ConId { get; set; }
}

public class GetConductorByIdQueryHandler : IRequestHandler<GetConductorByIdQuery, ConductorDto?>
{
    private readonly IConductorRepository _repository;

    public GetConductorByIdQueryHandler(IConductorRepository repository)
    {
        _repository = repository;
    }

    public async Task<ConductorDto?> Handle(GetConductorByIdQuery request, CancellationToken cancellationToken)
    {
        var conductor = await _repository.GetByIdAsync(request.ConId, cancellationToken);
        return conductor?.ToDto();
    }
}
