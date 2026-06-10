namespace JHT.Logistics.Domain.Entities;

public class RservicioConductor
{
    public int SerId { get; set; }
    public int ConId { get; set; }
    
    public DateTime FechaAsignacion { get; set; }
    public string UsuarioAsignador { get; set; } = null!;
    
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public bool AsignacionActiva { get; set; }
    public string? MotivoCambio { get; set; }

    // Navigation Properties
    public Servicio Servicio { get; set; } = null!;
    public Conductor Conductor { get; set; } = null!;
}
