using JHT.Logistics.Domain.Entities;

namespace JHT.Logistics.Application.Features.Servicios.DTOs;

public class ServicioDto
{
    public int SerId { get; set; }
    public string SerCodigo { get; set; } = null!;
    public int CliId { get; set; }
    public string SerTipoServicio { get; set; } = null!;
    public string? SerDescripcion { get; set; }
    public string? SerObservacion { get; set; }
    public string? SerPrioridad { get; set; }
    public DateTime? SerFechaProgramada { get; set; }
    public DateTime? SerFechaInicioReal { get; set; }
    public DateTime? SerFechaFinReal { get; set; }
    public int EstId { get; set; }
    public string? EstadoNombre { get; set; }
    public bool EstadoLogico { get; set; }

    public IEnumerable<DestinoDto> Destinos { get; set; } = new List<DestinoDto>();
    // Aquí podríamos agregar listas de Documentos, Historial, Vehiculos y Conductores asignados si se requiere en consultas pesadas.
}

public static class ServicioMapper
{
    public static ServicioDto ToDto(this Servicio servicio)
    {
        return new ServicioDto
        {
            SerId = servicio.SerId,
            SerCodigo = servicio.SerCodigo,
            CliId = servicio.CliId,
            SerTipoServicio = servicio.SerTipoServicio,
            SerDescripcion = servicio.SerDescripcion,
            SerObservacion = servicio.SerObservacion,
            SerPrioridad = servicio.SerPrioridad,
            SerFechaProgramada = servicio.SerFechaProgramada,
            SerFechaInicioReal = servicio.SerFechaInicioReal,
            SerFechaFinReal = servicio.SerFechaFinReal,
            EstId = servicio.EstId,
            EstadoNombre = servicio.EstadoServicio?.EstNombre,
            EstadoLogico = servicio.Estado,
            Destinos = servicio.Destinos?.Select(d => d.ToDto()).ToList() ?? new List<DestinoDto>()
        };
    }
}
