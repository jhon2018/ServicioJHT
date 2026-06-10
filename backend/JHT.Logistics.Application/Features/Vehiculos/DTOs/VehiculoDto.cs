using JHT.Logistics.Domain.Entities;

namespace JHT.Logistics.Application.Features.Vehiculos.DTOs;

public class VehiculoDto
{
    public int VehId { get; set; }
    public string VehPlaca { get; set; } = null!;
    public string VehMarca { get; set; } = null!;
    public string VehModelo { get; set; } = null!;
    public string VehTipo { get; set; } = null!;
    public string? VehCapacidad { get; set; }
    public bool Estado { get; set; }
}

public static class VehiculoMapper
{
    public static VehiculoDto ToDto(this Vehiculo vehiculo)
    {
        return new VehiculoDto
        {
            VehId = vehiculo.VehId,
            VehPlaca = vehiculo.VehPlaca,
            VehMarca = vehiculo.VehMarca,
            VehModelo = vehiculo.VehModelo,
            VehTipo = vehiculo.VehTipo,
            VehCapacidad = vehiculo.VehCapacidad,
            Estado = vehiculo.Estado
        };
    }
}
