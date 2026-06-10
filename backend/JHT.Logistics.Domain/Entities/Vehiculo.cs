using JHT.Logistics.Domain.Common;

namespace JHT.Logistics.Domain.Entities;

public class Vehiculo : AuditableEntity
{
    public int VehId { get; set; }
    public string VehPlaca { get; set; } = null!;
    public string VehMarca { get; set; } = null!;
    public string VehModelo { get; set; } = null!;
    public string VehTipo { get; set; } = null!;
    public string? VehCapacidad { get; set; }
}
