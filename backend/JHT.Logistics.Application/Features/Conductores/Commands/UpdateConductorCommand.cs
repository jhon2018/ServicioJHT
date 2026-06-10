using JHT.Logistics.Application.Features.Conductores.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Conductores.Commands;

public class UpdateConductorCommand : IRequest<ConductorDto>
{
    public int ConId { get; set; }
    public string? ConCodigoExterno { get; set; }
    public string ConTipo { get; set; } = null!;
    public string ConDni { get; set; } = null!;
    public string ConNombreCompleto { get; set; } = null!;
    public string ConTelefono { get; set; } = null!;
    public string? ConEmail { get; set; }
}

public class UpdateConductorCommandHandler : IRequestHandler<UpdateConductorCommand, ConductorDto>
{
    private readonly IConductorRepository _repository;

    public UpdateConductorCommandHandler(IConductorRepository repository)
    {
        _repository = repository;
    }

    public async Task<ConductorDto> Handle(UpdateConductorCommand request, CancellationToken cancellationToken)
    {
        var conductor = await _repository.GetByIdAsync(request.ConId, cancellationToken);

        if (conductor == null)
            throw new Exception($"Conductor con ID {request.ConId} no encontrado.");

        conductor.ConCodigoExterno = request.ConCodigoExterno;
        conductor.ConTipo = request.ConTipo.ToUpper();
        conductor.ConDni = request.ConDni;
        conductor.ConNombreCompleto = request.ConNombreCompleto;
        conductor.ConTelefono = request.ConTelefono;
        conductor.ConEmail = request.ConEmail;

        await _repository.UpdateAsync(conductor, cancellationToken);

        return conductor.ToDto();
    }
}
