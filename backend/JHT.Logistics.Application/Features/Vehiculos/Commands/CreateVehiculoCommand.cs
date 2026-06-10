using JHT.Logistics.Application.Features.Vehiculos.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using JHT.Logistics.Domain.Entities;
using MediatR;

namespace JHT.Logistics.Application.Features.Vehiculos.Commands;

public class CreateVehiculoCommand : IRequest<VehiculoDto>
{
    public string VehPlaca { get; set; } = null!;
    public string VehMarca { get; set; } = null!;
    public string VehModelo { get; set; } = null!;
    public string VehTipo { get; set; } = null!;
    public string? VehCapacidad { get; set; }
}

public class CreateVehiculoCommandHandler : IRequestHandler<CreateVehiculoCommand, VehiculoDto>
{
    private readonly IVehiculoRepository _repository;

    public CreateVehiculoCommandHandler(IVehiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<VehiculoDto> Handle(CreateVehiculoCommand request, CancellationToken cancellationToken)
    {
        var vehiculo = new Vehiculo
        {
            VehPlaca = request.VehPlaca.ToUpper(),
            VehMarca = request.VehMarca,
            VehModelo = request.VehModelo,
            VehTipo = request.VehTipo,
            VehCapacidad = request.VehCapacidad,
            Estado = true
        };

        await _repository.AddAsync(vehiculo, cancellationToken);

        return vehiculo.ToDto();
    }
}
