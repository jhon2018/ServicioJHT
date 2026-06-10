namespace JHT.Logistics.Domain.Entities;

public class TTrackingPublico
{
    public int TrkId { get; set; }
    public int SerId { get; set; }
    
    public string TrkToken { get; set; } = null!;
    public DateTime TrkFechaCreacion { get; set; }
    public bool TrkEstado { get; set; } // true = Activo, false = Expirado/Revocado

    // Navigation Property
    public Servicio Servicio { get; set; } = null!;
}
