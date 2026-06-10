using JHT.Logistics.Application.Interfaces.Repositories;
using JHT.Logistics.Domain.Entities;
using MediatR;

namespace JHT.Logistics.Application.Features.Servicios.Commands;

public class UpdateServicioEstadoCommand : IRequest<bool>
{
    public int SerId { get; set; }
    public int EstId { get; set; }
    public string? Observacion { get; set; }
}

public class UpdateServicioEstadoCommandHandler : IRequestHandler<UpdateServicioEstadoCommand, bool>
{
    private readonly IServicioRepository _repository;

    public UpdateServicioEstadoCommandHandler(IServicioRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateServicioEstadoCommand request, CancellationToken cancellationToken)
    {
        var servicio = await _repository.GetByIdAsync(request.SerId, cancellationToken);
        if (servicio == null) return false;

        // No actualizar si es el mismo estado
        if (servicio.EstId == request.EstId) return true;

        servicio.EstId = request.EstId;

        servicio.HistorialEstados.Add(new HservicioEstado
        {
            EstId = request.EstId,
            HseFechaHora = DateTime.UtcNow,
            HseObservacion = request.Observacion ?? "Actualización de estado",
            UsuarioRegistro = "SISTEMA" // Idealmente sacado del Claim JWT
        });

        await _repository.UpdateAsync(servicio, cancellationToken);
        return true;
    }
}
