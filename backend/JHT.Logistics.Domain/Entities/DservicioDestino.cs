using JHT.Logistics.Domain.Common;

namespace JHT.Logistics.Domain.Entities;

public class DservicioDestino : AuditableEntity
{
    public int SerdesId { get; set; }
    public int SerId { get; set; }
    public int SerdesSecuencia { get; set; }
    
    public string SerdesDestinatario { get; set; } = null!;
    public string? SerdesRuc { get; set; }
    public string SerdesDireccion { get; set; } = null!;
    public string? SerdesReferencia { get; set; }
    public string? SerdesContacto { get; set; }
    public string? SerdesTelefono { get; set; }
    
    public string SerdesEstado { get; set; } = null!; // PENDIENTE, EN_RUTA, ENTREGADO, FALLIDO
    
    // Navigation Property
    public Servicio Servicio { get; set; } = null!;
}
