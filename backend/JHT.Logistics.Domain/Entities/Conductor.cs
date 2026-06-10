using JHT.Logistics.Domain.Common;

namespace JHT.Logistics.Domain.Entities;

public class Conductor : AuditableEntity
{
    public int ConId { get; set; }
    public string? ConCodigoExterno { get; set; }
    public string ConTipo { get; set; } = null!; // I = Interno, E = Externo
    public string ConDni { get; set; } = null!;
    public string ConNombreCompleto { get; set; } = null!;
    public string ConTelefono { get; set; } = null!;
    public string? ConEmail { get; set; }
}
