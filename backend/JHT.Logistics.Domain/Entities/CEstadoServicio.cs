using JHT.Logistics.Domain.Common;

namespace JHT.Logistics.Domain.Entities;

public class CEstadoServicio
{
    public int EstId { get; set; }
    public string EstCodigo { get; set; } = null!;
    public string EstNombre { get; set; } = null!;
}
