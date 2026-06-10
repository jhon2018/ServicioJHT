using JHT.Logistics.Domain.Common;

namespace JHT.Logistics.Domain.Entities;

public class Servicio : AuditableEntity
{
    public int SerId { get; set; }
    public string SerCodigo { get; set; } = null!;
    public int CliId { get; set; }
    
    public string SerTipoServicio { get; set; } = null!;
    public string? SerDescripcion { get; set; }
    public string? SerObservacion { get; set; }
    public string? SerPrioridad { get; set; } // ALTA, MEDIA, BAJA
    
    public DateTime? SerFechaProgramada { get; set; }
    public DateTime? SerFechaInicioReal { get; set; }
    public DateTime? SerFechaFinReal { get; set; }

    public int EstId { get; set; } // Estado actual del servicio
    
    // Navigation Properties
    public Cliente Cliente { get; set; } = null!;
    public CEstadoServicio EstadoServicio { get; set; } = null!;

    public ICollection<DservicioDestino> Destinos { get; set; } = new List<DservicioDestino>();
    public ICollection<DservicioDocumento> Documentos { get; set; } = new List<DservicioDocumento>();
    public ICollection<HservicioEstado> HistorialEstados { get; set; } = new List<HservicioEstado>();
    public ICollection<RservicioConductor> ConductoresAsignados { get; set; } = new List<RservicioConductor>();
    public ICollection<RservicioVehiculo> VehiculosAsignados { get; set; } = new List<RservicioVehiculo>();
    public TTrackingPublico? TrackingPublico { get; set; }
}
