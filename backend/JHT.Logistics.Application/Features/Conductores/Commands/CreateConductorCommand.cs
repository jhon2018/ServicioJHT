using JHT.Logistics.Application.Features.Conductores.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using JHT.Logistics.Domain.Entities;
using MediatR;

namespace JHT.Logistics.Application.Features.Conductores.Commands;

public class CreateConductorCommand : IRequest<ConductorDto>
{
    public string? ConCodigoExterno { get; set; }
    public string ConTipo { get; set; } = null!;
    public string ConDni { get; set; } = null!;
    public string ConNombreCompleto { get; set; } = null!;
    public string ConTelefono { get; set; } = null!;
    public string? ConEmail { get; set; }
}

public class CreateConductorCommandHandler : IRequestHandler<CreateConductorCommand, ConductorDto>
{
    private readonly IConductorRepository _repository;

    public CreateConductorCommandHandler(IConductorRepository repository)
    {
        _repository = repository;
    }

    public async Task<ConductorDto> Handle(CreateConductorCommand request, CancellationToken cancellationToken)
    {
        var conductor = new Conductor
        {
            ConCodigoExterno = request.ConCodigoExterno,
            ConTipo = request.ConTipo.ToUpper(),
            ConDni = request.ConDni,
            ConNombreCompleto = request.ConNombreCompleto,
            ConTelefono = request.ConTelefono,
            ConEmail = request.ConEmail,
            Estado = true
        };

        await _repository.AddAsync(conductor, cancellationToken);

        return conductor.ToDto();
    }
}
