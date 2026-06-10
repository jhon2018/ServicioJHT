using JHT.Logistics.Domain.Entities;

namespace JHT.Logistics.Application.Features.Conductores.DTOs;

public class ConductorDto
{
    public int ConId { get; set; }
    public string? ConCodigoExterno { get; set; }
    public string ConTipo { get; set; } = null!;
    public string ConDni { get; set; } = null!;
    public string ConNombreCompleto { get; set; } = null!;
    public string ConTelefono { get; set; } = null!;
    public string? ConEmail { get; set; }
    public bool Estado { get; set; }
}

public static class ConductorMapper
{
    public static ConductorDto ToDto(this Conductor conductor)
    {
        return new ConductorDto
        {
            ConId = conductor.ConId,
            ConCodigoExterno = conductor.ConCodigoExterno,
            ConTipo = conductor.ConTipo,
            ConDni = conductor.ConDni,
            ConNombreCompleto = conductor.ConNombreCompleto,
            ConTelefono = conductor.ConTelefono,
            ConEmail = conductor.ConEmail,
            Estado = conductor.Estado
        };
    }
}
