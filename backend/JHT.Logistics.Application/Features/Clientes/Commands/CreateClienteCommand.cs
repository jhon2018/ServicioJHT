using JHT.Logistics.Application.Features.Clientes.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using JHT.Logistics.Domain.Entities;
using MediatR;

namespace JHT.Logistics.Application.Features.Clientes.Commands;

public class CreateClienteCommand : IRequest<ClienteDto>
{
    public string CliTipoDocumento { get; set; } = null!;
    public string CliNumeroDocumento { get; set; } = null!;
    public string CliRazonSocial { get; set; } = null!;
    public string? CliNombreComercial { get; set; }
    public string? CliDireccion { get; set; }
    public string? CliTelefono { get; set; }
    public string? CliEmail { get; set; }
}

public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, ClienteDto>
{
    private readonly IClienteRepository _repository;

    public CreateClienteCommandHandler(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClienteDto> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente
        {
            CliTipoDocumento = request.CliTipoDocumento,
            CliNumeroDocumento = request.CliNumeroDocumento,
            CliRazonSocial = request.CliRazonSocial,
            CliNombreComercial = request.CliNombreComercial,
            CliDireccion = request.CliDireccion,
            CliTelefono = request.CliTelefono,
            CliEmail = request.CliEmail,
            Estado = true
        };

        await _repository.AddAsync(cliente, cancellationToken);

        return cliente.ToDto();
    }
}
