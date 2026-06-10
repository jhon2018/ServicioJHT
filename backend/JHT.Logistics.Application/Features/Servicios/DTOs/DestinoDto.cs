using JHT.Logistics.Domain.Entities;

namespace JHT.Logistics.Application.Features.Servicios.DTOs;

public class DestinoDto
{
    public int SerdesId { get; set; }
    public int SerdesSecuencia { get; set; }
    public string SerdesDestinatario { get; set; } = null!;
    public string? SerdesRuc { get; set; }
    public string SerdesDireccion { get; set; } = null!;
    public string? SerdesReferencia { get; set; }
    public string? SerdesContacto { get; set; }
    public string? SerdesTelefono { get; set; }
    public string SerdesEstado { get; set; } = null!;
}

public static class DestinoMapper
{
    public static DestinoDto ToDto(this DservicioDestino destino)
    {
        return new DestinoDto
        {
            SerdesId = destino.SerdesId,
            SerdesSecuencia = destino.SerdesSecuencia,
            SerdesDestinatario = destino.SerdesDestinatario,
            SerdesRuc = destino.SerdesRuc,
            SerdesDireccion = destino.SerdesDireccion,
            SerdesReferencia = destino.SerdesReferencia,
            SerdesContacto = destino.SerdesContacto,
            SerdesTelefono = destino.SerdesTelefono,
            SerdesEstado = destino.SerdesEstado
        };
    }
}
