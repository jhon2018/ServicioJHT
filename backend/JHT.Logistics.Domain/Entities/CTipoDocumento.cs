using JHT.Logistics.Domain.Common;

namespace JHT.Logistics.Domain.Entities;

public class CTipoDocumento
{
    public int TipdocId { get; set; }
    public string TipdocCodigo { get; set; } = null!;
    public string TipdocNombre { get; set; } = null!;
}
