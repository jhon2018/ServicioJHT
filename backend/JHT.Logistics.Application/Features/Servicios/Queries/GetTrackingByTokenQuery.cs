using JHT.Logistics.Application.Features.Servicios.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Servicios.Queries;

public class GetTrackingByTokenQuery : IRequest<TrackingDto?>
{
    public string Token { get; set; } = null!;
}

public class GetTrackingByTokenQueryHandler : IRequestHandler<GetTrackingByTokenQuery, TrackingDto?>
{
    private readonly IServicioRepository _repository;

    public GetTrackingByTokenQueryHandler(IServicioRepository repository)
    {
        _repository = repository;
    }

    public async Task<TrackingDto?> Handle(GetTrackingByTokenQuery request, CancellationToken cancellationToken)
    {
        var tracking = await _repository.GetTrackingByTokenAsync(request.Token, cancellationToken);
        
        if (tracking == null)
            return null;

        return tracking.ToTrackingDto();
    }
}
