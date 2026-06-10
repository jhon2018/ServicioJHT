using JHT.Logistics.Application.Features.Vehiculos.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Vehiculos.Commands;

public class UpdateVehiculoCommand : IRequest<VehiculoDto>
{
    public int VehId { get; set; }
    public string VehPlaca { get; set; } = null!;
    public string VehMarca { get; set; } = null!;
    public string VehModelo { get; set; } = null!;
    public string VehTipo { get; set; } = null!;
    public string? VehCapacidad { get; set; }
    public bool Estado { get; set; }
}

public class UpdateVehiculoCommandHandler : IRequestHandler<UpdateVehiculoCommand, VehiculoDto>
{
    private readonly IVehiculoRepository _repository;

    public UpdateVehiculoCommandHandler(IVehiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<VehiculoDto> Handle(UpdateVehiculoCommand request, CancellationToken cancellationToken)
    {
        var vehiculo = await _repository.GetByIdAsync(request.VehId, cancellationToken);

        if (vehiculo == null)
            throw new Exception($"Vehículo con ID {request.VehId} no encontrado.");

        vehiculo.VehPlaca = request.VehPlaca.ToUpper();
        vehiculo.VehMarca = request.VehMarca;
        vehiculo.VehModelo = request.VehModelo;
        vehiculo.VehTipo = request.VehTipo;
        vehiculo.VehCapacidad = request.VehCapacidad;
        vehiculo.Estado = request.Estado;

        await _repository.UpdateAsync(vehiculo, cancellationToken);

        return vehiculo.ToDto();
    }
}
