using JHT.Logistics.Application.Features.Conductores.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Conductores.Queries;

public class GetAllConductoresQuery : IRequest<IEnumerable<ConductorDto>>
{
}

public class GetAllConductoresQueryHandler : IRequestHandler<GetAllConductoresQuery, IEnumerable<ConductorDto>>
{
    private readonly IConductorRepository _repository;

    public GetAllConductoresQueryHandler(IConductorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ConductorDto>> Handle(GetAllConductoresQuery request, CancellationToken cancellationToken)
    {
        var conductores = await _repository.GetAllAsync(cancellationToken);
        return conductores.Select(c => c.ToDto());
    }
}
