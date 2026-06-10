using JHT.Logistics.Domain.Entities;

namespace JHT.Logistics.Application.Features.Clientes.DTOs;

public class ClienteDto
{
    public int CliId { get; set; }
    public string CliTipoDocumento { get; set; } = null!;
    public string CliNumeroDocumento { get; set; } = null!;
    public string CliRazonSocial { get; set; } = null!;
    public string? CliNombreComercial { get; set; }
    public string? CliDireccion { get; set; }
    public string? CliTelefono { get; set; }
    public string? CliEmail { get; set; }
    public bool Estado { get; set; }
}

public static class ClienteMapper
{
    public static ClienteDto ToDto(this Cliente cliente)
    {
        return new ClienteDto
        {
            CliId = cliente.CliId,
            CliTipoDocumento = cliente.CliTipoDocumento,
            CliNumeroDocumento = cliente.CliNumeroDocumento,
            CliRazonSocial = cliente.CliRazonSocial,
            CliNombreComercial = cliente.CliNombreComercial,
            CliDireccion = cliente.CliDireccion,
            CliTelefono = cliente.CliTelefono,
            CliEmail = cliente.CliEmail,
            Estado = cliente.Estado
        };
    }
}
