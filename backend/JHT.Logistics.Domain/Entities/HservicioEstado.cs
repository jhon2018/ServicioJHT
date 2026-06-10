namespace JHT.Logistics.Domain.Entities;

public class HservicioEstado
{
    public int HseId { get; set; }
    public int SerId { get; set; }
    public int EstId { get; set; }
    
    public DateTime HseFechaHora { get; set; }
    public string? HseObservacion { get; set; }
    public string UsuarioRegistro { get; set; } = null!;

    // Navigation Properties
    public Servicio Servicio { get; set; } = null!;
    public CEstadoServicio EstadoServicio { get; set; } = null!;
}
