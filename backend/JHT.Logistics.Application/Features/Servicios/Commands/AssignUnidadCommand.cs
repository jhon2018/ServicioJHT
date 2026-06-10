using JHT.Logistics.Application.Interfaces.Repositories;
using JHT.Logistics.Domain.Entities;
using MediatR;

namespace JHT.Logistics.Application.Features.Servicios.Commands;

public class AssignUnidadCommand : IRequest<bool>
{
    public int SerId { get; set; }
    public int ConId { get; set; }
    public int VehId { get; set; }
}

public class AssignUnidadCommandHandler : IRequestHandler<AssignUnidadCommand, bool>
{
    private readonly IServicioRepository _repository;

    public AssignUnidadCommandHandler(IServicioRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(AssignUnidadCommand request, CancellationToken cancellationToken)
    {
        var servicio = await _repository.GetByIdAsync(request.SerId, cancellationToken);
        if (servicio == null) return false;

        // Desactivar asignaciones previas si existen
        foreach (var conductor in servicio.ConductoresAsignados)
        {
            conductor.AsignacionActiva = false;
            conductor.FechaFin = DateTime.UtcNow;
            conductor.MotivoCambio = "Reasignación de unidad";
        }

        foreach (var vehiculo in servicio.VehiculosAsignados)
        {
            vehiculo.AsignacionActiva = false;
            vehiculo.FechaFin = DateTime.UtcNow;
            vehiculo.MotivoCambio = "Reasignación de unidad";
        }

        // Agregar nueva asignación
        servicio.ConductoresAsignados.Add(new RservicioConductor
        {
            ConId = request.ConId,
            FechaAsignacion = DateTime.UtcNow,
            UsuarioAsignador = "SISTEMA", // Esto luego se toma del Claim JWT
            AsignacionActiva = true
        });

        servicio.VehiculosAsignados.Add(new RservicioVehiculo
        {
            VehId = request.VehId,
            FechaAsignacion = DateTime.UtcNow,
            UsuarioAsignador = "SISTEMA", // Esto luego se toma del Claim JWT
            AsignacionActiva = true
        });

        // Cambiar estado a Unidad Asignada (EstId = 3)
        if (servicio.EstId < 3)
        {
            servicio.EstId = 3;
            servicio.HistorialEstados.Add(new HservicioEstado
            {
                EstId = 3,
                HseFechaHora = DateTime.UtcNow,
                HseObservacion = "Unidad y Conductor asignados",
                UsuarioRegistro = "SISTEMA"
            });
        }

        await _repository.UpdateAsync(servicio, cancellationToken);
        return true;
    }
}
