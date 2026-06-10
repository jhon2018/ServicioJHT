using JHT.Logistics.Domain.Entities;

namespace JHT.Logistics.Application.Features.Servicios.DTOs;

public class TrackingDto
{
    public string SerCodigo { get; set; } = null!;
    public string SerTipoServicio { get; set; } = null!;
    public string? EstadoNombre { get; set; }
    public DateTime? FechaProgramada { get; set; }
    public IEnumerable<DestinoTrackingDto> Destinos { get; set; } = new List<DestinoTrackingDto>();
    public IEnumerable<HistorialTrackingDto> Historial { get; set; } = new List<HistorialTrackingDto>();
}

public class DestinoTrackingDto
{
    public int Secuencia { get; set; }
    public string Destinatario { get; set; } = null!;
    public string Estado { get; set; } = null!;
}

public class HistorialTrackingDto
{
    public DateTime FechaHora { get; set; }
    public string EstadoNombre { get; set; } = null!;
    public string? Observacion { get; set; }
}

public static class TrackingMapper
{
    public static TrackingDto ToTrackingDto(this TTrackingPublico tracking)
    {
        var servicio = tracking.Servicio;
        return new TrackingDto
        {
            SerCodigo = servicio.SerCodigo,
            SerTipoServicio = servicio.SerTipoServicio,
            EstadoNombre = servicio.EstadoServicio?.EstNombre,
            FechaProgramada = servicio.SerFechaProgramada,
            Destinos = servicio.Destinos.Select(d => new DestinoTrackingDto
            {
                Secuencia = d.SerdesSecuencia,
                Destinatario = d.SerdesDestinatario,
                Estado = d.SerdesEstado
            }).OrderBy(d => d.Secuencia).ToList(),
            Historial = servicio.HistorialEstados.Select(h => new HistorialTrackingDto
            {
                FechaHora = h.HseFechaHora,
                EstadoNombre = h.EstadoServicio.EstNombre,
                Observacion = h.HseObservacion
            }).OrderByDescending(h => h.FechaHora).ToList()
        };
    }
}
