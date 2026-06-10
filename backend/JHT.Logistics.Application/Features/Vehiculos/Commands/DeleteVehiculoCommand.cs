using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Vehiculos.Commands;

public class DeleteVehiculoCommand : IRequest<bool>
{
    public int VehId { get; set; }
}

public class DeleteVehiculoCommandHandler : IRequestHandler<DeleteVehiculoCommand, bool>
{
    private readonly IVehiculoRepository _repository;

    public DeleteVehiculoCommandHandler(IVehiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteVehiculoCommand request, CancellationToken cancellationToken)
    {
        var vehiculo = await _repository.GetByIdAsync(request.VehId, cancellationToken);

        if (vehiculo == null)
            throw new Exception($"Vehículo con ID {request.VehId} no encontrado.");

        await _repository.DeleteAsync(vehiculo, cancellationToken);

        return true;
    }
}
