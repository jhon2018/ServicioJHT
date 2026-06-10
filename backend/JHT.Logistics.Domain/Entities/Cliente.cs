using JHT.Logistics.Domain.Common;

namespace JHT.Logistics.Domain.Entities;

public class Cliente : AuditableEntity
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
