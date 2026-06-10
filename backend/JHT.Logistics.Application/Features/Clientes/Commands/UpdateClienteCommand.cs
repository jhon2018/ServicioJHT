using JHT.Logistics.Application.Features.Clientes.DTOs;
using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Clientes.Commands;

public class UpdateClienteCommand : IRequest<ClienteDto>
{
    public int CliId { get; set; }
    public string CliTipoDocumento { get; set; } = null!;
    public string CliNumeroDocumento { get; set; } = null!;
    public string CliRazonSocial { get; set; } = null!;
    public string? CliNombreComercial { get; set; }
    public string? CliDireccion { get; set; }
    public string? CliTelefono { get; set; }
    public string? CliEmail { get; set; }
}

public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, ClienteDto>
{
    private readonly IClienteRepository _repository;

    public UpdateClienteCommandHandler(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<ClienteDto> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = await _repository.GetByIdAsync(request.CliId, cancellationToken);

        if (cliente == null)
            throw new Exception($"Cliente con ID {request.CliId} no encontrado.");

        cliente.CliTipoDocumento = request.CliTipoDocumento;
        cliente.CliNumeroDocumento = request.CliNumeroDocumento;
        cliente.CliRazonSocial = request.CliRazonSocial;
        cliente.CliNombreComercial = request.CliNombreComercial;
        cliente.CliDireccion = request.CliDireccion;
        cliente.CliTelefono = request.CliTelefono;
        cliente.CliEmail = request.CliEmail;

        await _repository.UpdateAsync(cliente, cancellationToken);

        return cliente.ToDto();
    }
}
